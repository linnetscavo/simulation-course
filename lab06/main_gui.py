# main_gui.py
import tkinter as tk
from tkinter import ttk, messagebox
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import math

from rng import MultiplicativeCongruentialGenerator
from stats_utils import (
    generate_discrete_rv, 
    generate_normal_rv_box_muller, 
    calculate_mean_variance, 
    chi_squared_test,
    create_histogram_data
)

class SimulationApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Лабораторная работа 6: Моделирование СВ")
        self.root.geometry("1000x700")

        self.tab_control = ttk.Notebook(root)
        
        self.tab_discrete = ttk.Frame(self.tab_control)
        self.tab_continuous = ttk.Frame(self.tab_control)
        
        self.tab_control.add(self.tab_discrete, text='Дискретная СВ')
        self.tab_control.add(self.tab_continuous, text='Непрерывная (Нормальная) СВ')
        self.tab_control.pack(expand=1, fill="both")

        self.setup_discrete_tab()
        self.setup_continuous_tab()

    def setup_discrete_tab(self):
        frame_input = ttk.LabelFrame(self.tab_discrete, text="Параметры распределения")
        frame_input.pack(fill="x", padx=10, pady=5)

        ttk.Label(frame_input, text="Значения (через запятую):").grid(row=0, column=0, sticky="w", padx=5, pady=5)
        self.entry_values = ttk.Entry(frame_input, width=40)
        self.entry_values.insert(0, "1, 2, 3, 4, 5")
        self.entry_values.grid(row=0, column=1, padx=5, pady=5)

        ttk.Label(frame_input, text="Вероятности (через запятую):").grid(row=1, column=0, sticky="w", padx=5, pady=5)
        self.entry_probs = ttk.Entry(frame_input, width=40)
        self.entry_probs.insert(0, "0.2, 0.2, 0.2, 0.2, 0.2")
        self.entry_probs.grid(row=1, column=1, padx=5, pady=5)

        btn_run = ttk.Button(frame_input, text="Запустить моделирование", command=self.run_discrete_simulation)
        btn_run.grid(row=2, column=0, columnspan=2, pady=10)

        cols = ("N", "Mean", "Mean Err %", "Var", "Var Err %", "Chi-Sq", "Chi-Crit", "Result")
        self.tree = ttk.Treeview(self.tab_discrete, columns=cols, show='headings')
        for col in cols:
            self.tree.heading(col, text=col)
            self.tree.column(col, width=80)
        self.tree.pack(fill="both", expand=True, padx=10, pady=5)

    def run_discrete_simulation(self):
        try:
            values_str = self.entry_values.get().split(',')
            probs_str = self.entry_probs.get().split(',')
            
            values = [float(v.strip()) for v in values_str]
            probs = [float(p.strip()) for p in probs_str]
            
            if abs(sum(probs) - 1.0) > 1e-5:
                messagebox.showerror("Ошибка", "Сумма вероятностей должна быть равна 1!")
                return
                
            theor_mean = sum(v * p for v, p in zip(values, probs))
            theor_var = sum((v ** 2) * p for v, p in zip(values, probs)) - theor_mean ** 2
            
            for item in self.tree.get_children():
                self.tree.delete(item)
                
            sample_sizes = [10, 100, 1000, 10000]
            rng = MultiplicativeCongruentialGenerator(seed=12345) 
            
            for n in sample_sizes:
                data = []
                for _ in range(n):
                    val = generate_discrete_rv(rng, values, probs)
                    data.append(val)
                
                emp_mean, emp_var = calculate_mean_variance(data)
                
                mean_err = abs(emp_mean - theor_mean) / theor_mean * 100 if theor_mean != 0 else 0
                var_err = abs(emp_var - theor_var) / theor_var * 100 if theor_var != 0 else 0
                
                chi_res = chi_squared_test(data, values, probs)
                chi_stat = chi_res['statistic']

                from scipy.stats import chi2 
                crit_val = chi2.ppf(0.95, chi_res['df'])
                
                result_text = "H0 принята" if chi_stat < crit_val else "H0 отвергнута"
                
                self.tree.insert("", "end", values=(
                    n, 
                    f"{emp_mean:.3f}", 
                    f"{mean_err:.2f}%", 
                    f"{emp_var:.3f}", 
                    f"{var_err:.2f}%", 
                    f"{chi_stat:.2f}", 
                    f"{crit_val:.2f}", 
                    result_text
                ))
                
        except Exception as e:
            messagebox.showerror("Ошибка", str(e))

    def setup_continuous_tab(self):
        frame_ctrl = ttk.Frame(self.tab_continuous)
        frame_ctrl.pack(fill="x", padx=10, pady=5)
        
        ttk.Label(frame_ctrl, text="Mu (среднее):").pack(side="left", padx=5)
        self.ent_mu = ttk.Entry(frame_ctrl, width=10)
        self.ent_mu.insert(0, "0")
        self.ent_mu.pack(side="left", padx=5)
        
        ttk.Label(frame_ctrl, text="Sigma (стандартное отклонение):").pack(side="left", padx=5)
        self.ent_sigma = ttk.Entry(frame_ctrl, width=10)
        self.ent_sigma.insert(0, "1")
        self.ent_sigma.pack(side="left", padx=5)
        
        ttk.Label(frame_ctrl, text="N (объем выборки):").pack(side="left", padx=5)
        self.ent_n = ttk.Entry(frame_ctrl, width=10)
        self.ent_n.insert(0, "1000")
        self.ent_n.pack(side="left", padx=5)
        
        btn_plot = ttk.Button(frame_ctrl, text="Построить гистограмму", command=self.plot_normal_histogram)
        btn_plot.pack(side="left", padx=10)
        
        # Место для графика
        self.fig_frame = ttk.Frame(self.tab_continuous)
        self.fig_frame.pack(fill="both", expand=True, padx=10, pady=5)

    def plot_normal_histogram(self):
        try:
            mu = float(self.ent_mu.get())
            sigma = float(self.ent_sigma.get())
            n = int(self.ent_n.get())
            
            rng = MultiplicativeCongruentialGenerator(seed=54321)
            data = [generate_normal_rv_box_muller(rng, mu, sigma) for _ in range(n)]
            
            for widget in self.fig_frame.winfo_children():
                widget.destroy()
                
            fig, ax = plt.subplots(figsize=(8, 4))
            ax.hist(data, bins=30, density=True, alpha=0.6, color='g', label='Эмпирическая')
            
            xmin, xmax = ax.get_xlim()
            x = np.linspace(xmin, xmax, 100)
            pdf = (1/(sigma * np.sqrt(2 * np.pi))) * np.exp(-(x - mu)**2 / (2 * sigma**2))
            ax.plot(x, pdf, 'k', linewidth=2, label='Теоретическая')
            
            ax.set_title(f"Normal Distribution N={n}, Mu={mu}, Sigma={sigma}")
            ax.legend()
            
            canvas = FigureCanvasTkAgg(fig, master=self.fig_frame)
            canvas.draw()
            canvas.get_tk_widget().pack(fill="both", expand=True)
            
        except Exception as e:
            messagebox.showerror("Ошибка", str(e))

if __name__ == "__main__":
    try:
        import numpy as np
    except ImportError:
        print("Установите numpy: pip install numpy")
        
    try:
        from scipy.stats import chi2
    except ImportError:
        print("Установите scipy: pip install scipy (для точного расчета критических значений)")

    root = tk.Tk()
    app = SimulationApp(root)
    root.mainloop()