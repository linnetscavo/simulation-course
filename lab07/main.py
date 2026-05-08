import numpy as np
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import tkinter as tk
from tkinter import ttk, messagebox, filedialog
import pandas as pd
from scipy.linalg import null_space
import sys


DEFAULT_Q = [
    [-0.6,  0.4,  0.2],
    [ 0.3, -0.8,  0.5],
    [ 0.1,  0.3, -0.4]
]
STATE_NAMES = ["Ясно (1)", "Облачно (2)", "Пасмурно (3)"]
COLORS = ['#FFD700', '#A9A9A9', '#708090']

class MarkovWeatherApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Лабораторная: Марковская модель погоды (Непрерывное время)")
        self.root.geometry("1200x800")

        self.is_running = False
        self.simulation_finished = False
        self.current_state = 0 # Индекс 0-2
        self.current_time = 0.0
        self.steps_done = 0
        self.total_steps = 1000
        
        self.time_in_states = np.zeros(3)
        self.history_states = [] 
        self.history_times = []  
        self.conv_steps = []     
        self.conv_probs = [[] for _ in range(3)] 
        
        self.log_data = []

        self.Q_matrix = np.array(DEFAULT_Q)
        self.pi_theoretical = self.calculate_stationary(self.Q_matrix)

        self.current_state = int(np.argmax(self.pi_theoretical))

        main_frame = ttk.Frame(root)
        main_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)


        left_panel = ttk.Frame(main_frame, width=300)
        left_panel.pack(side=tk.LEFT, fill=tk.Y, padx=(0, 10))
        left_panel.pack_propagate(False)

        right_panel = ttk.Frame(main_frame)
        right_panel.pack(side=tk.RIGHT, fill=tk.BOTH, expand=True)

        self.create_left_panel(left_panel)
        self.create_right_panel(right_panel)

    def exit_app(self):
        self.is_running = False
        self.root.destroy()
        sys.exit(0)


    def create_left_panel(self, parent):
        ttk.Label(parent, text="Параметры модели", font=('Arial', 12, 'bold')).pack(pady=10)
        frame_matrix = ttk.LabelFrame(parent, text="Матрица интенсивностей Q")
        frame_matrix.pack(fill=tk.X, padx=5, pady=5)
        
        self.entries = []
        for i in range(3):
            row_frame = ttk.Frame(frame_matrix)
            row_frame.pack(pady=2)
            row_entries = []
            for j in range(3):
                e = ttk.Entry(row_frame, width=8, justify='center')
                e.insert(0, f"{DEFAULT_Q[i][j]}")
                e.pack(side=tk.LEFT, padx=2)
                row_entries.append(e)
            self.entries.append(row_entries)
            
        ttk.Button(frame_matrix, text="Применить матрицу", command=self.apply_matrix).pack(pady=5)

        frame_n = ttk.Frame(parent)
        frame_n.pack(fill=tk.X, padx=5, pady=5)
        ttk.Label(frame_n, text="Количество переходов (N):").pack(anchor=tk.W)
        self.var_n = tk.StringVar(value="1000")
        ttk.Entry(frame_n, textvariable=self.var_n).pack(fill=tk.X, pady=2)

  
        self.status_var = tk.StringVar(value="Прошло дней: 0.00")
        ttk.Label(parent, textvariable=self.status_var, font=('Courier', 10), foreground='blue').pack(pady=10)

        frame_speed = ttk.Frame(parent)
        frame_speed.pack(fill=tk.X, padx=5, pady=5)
        ttk.Label(frame_speed, text="Задержка анимации:").pack(anchor=tk.W)
        self.var_speed = tk.IntVar(value=20)
        scale = ttk.Scale(frame_speed, from_=10, to=200, variable=self.var_speed, orient=tk.HORIZONTAL)
        scale.pack(fill=tk.X)

        btn_frame = ttk.Frame(parent)
        btn_frame.pack(fill=tk.X, padx=5, pady=10)
        
        ttk.Button(btn_frame, text="Старт", command=self.start_sim).pack(fill=tk.X, pady=2)
        ttk.Button(btn_frame, text="Стоп", command=self.stop_sim).pack(fill=tk.X, pady=2)
        ttk.Button(btn_frame, text="Сброс", command=self.reset_sim).pack(fill=tk.X, pady=2)
        ttk.Button(btn_frame, text="Сохранить CSV", command=self.save_csv).pack(fill=tk.X, pady=2)

        ttk.Frame(btn_frame, height=10).pack(fill=tk.X) 
        btn_exit = ttk.Button(btn_frame, text=" Закрыть программу", command=self.exit_app)
        btn_exit.pack(fill=tk.X, pady=2)

    def create_right_panel(self, parent):
        self.fig, (self.ax1, self.ax2) = plt.subplots(2, 1, figsize=(8, 8))
        self.canvas = FigureCanvasTkAgg(self.fig, master=parent)
        self.canvas.get_tk_widget().pack(fill=tk.BOTH, expand=True)

        self.ax1.set_title("Поток состояний (последние 500 шагов)")
        self.ax1.set_ylabel("Состояние")
        self.ax1.set_yticks([0, 1, 2])
        self.ax1.set_yticklabels(STATE_NAMES)
        self.ax1.grid(True, linestyle='--', alpha=0.5)
        self.line_state, = self.ax1.plot([], [], drawstyle='steps-post', color='black', linewidth=1.5)

        self.ax2.set_title("Сходимость эмпирических долей к теоретическим")
        self.ax2.set_xlabel("Номер перехода")
        self.ax2.set_ylabel("Доля времени")
        self.ax2.set_ylim(0, 1)
        self.ax2.grid(True, linestyle='--', alpha=0.5)
        
        self.lines_conv = []
        for i in range(3):
            l, = self.ax2.plot([], [], color=COLORS[i], label=STATE_NAMES[i], linewidth=2)
            self.lines_conv.append(l)
            self.ax2.axhline(y=self.pi_theoretical[i], color=COLORS[i], linestyle='--', alpha=0.5)
        self.ax2.legend(loc='right')
        self.error_text = self.ax2.text(0.05, 0.95, '', transform=self.ax2.transAxes, fontsize=9,
                                verticalalignment='top', bbox=dict(boxstyle='round,pad=0.3', fc='yellow', alpha=0.5))

        self.fig.tight_layout()

    def calculate_stationary(self, Q):
        n = Q.shape[0]
        A = Q.T.copy()
        A[-1, :] = 1.0
        b = np.zeros(n)
        b[-1] = 1.0
        try:
            return np.linalg.solve(A, b)
        except:
            ns = null_space(Q.T)
            pi = ns[:, 0].real
            return pi / pi.sum()

    def apply_matrix(self):
        try:
            new_q = np.zeros((3,3))
            for i in range(3):
                for j in range(3):
                    val = float(self.entries[i][j].get())
                    new_q[i][j] = val

            diag = np.diag(new_q)
            if np.any(diag > 1e-6):
                raise ValueError("Диагональные элементы должны быть <= 0")
            
            row_sums = new_q.sum(axis=1)
            if not np.allclose(row_sums, 0, atol=1e-6):
                raise ValueError(f"Сумма строк должна быть 0.")
            

            self.Q_matrix = new_q
            self.pi_theoretical = self.calculate_stationary(self.Q_matrix)
            self.reset_sim()
            messagebox.showinfo("Успех", "Матрица обновлена и проверена.")
        except Exception as e:
            messagebox.showerror("Ошибка ввода", str(e))

    def get_next_step_data(self):
        lambda_i = -self.Q_matrix[self.current_state, self.current_state]
        
        if lambda_i < 1e-9: 
            tau = 1.0 
            next_state = self.current_state
        else:
            tau = np.random.exponential(1.0 / lambda_i)

            probs = self.Q_matrix[self.current_state].copy()
            probs[self.current_state] = 0.0
            probs_sum = probs.sum()
            if probs_sum > 1e-9:
                probs /= probs_sum
                next_state = np.random.choice(3, p=probs)
            else:
                next_state = self.current_state

        return tau, next_state

    def update_simulation_step(self):
        if not self.is_running or self.steps_done >= self.total_steps:
            self.is_running = False
            self.simulation_finished = True
            return

        tau, next_state = self.get_next_step_data()

        self.log_data.append({
            'Step': self.steps_done + 1,
            'State_Code': self.current_state + 1,
            'State_Name': STATE_NAMES[self.current_state],
            'Duration_Days': round(tau, 4),
            'Total_Time': round(self.current_time + tau, 4)
        })

        self.time_in_states[self.current_state] += tau
        self.current_time += tau
        self.steps_done += 1

        start_time = self.current_time - tau
        self.history_times.extend([start_time, self.current_time])
        self.history_states.extend([self.current_state, self.current_state])
        
        MAX_POINTS = 500
        if len(self.history_times) > MAX_POINTS:
            self.history_times = self.history_times[-MAX_POINTS:]
            self.history_states = self.history_states[-MAX_POINTS:]
        
        total_t = self.current_time
        if total_t > 0:
            curr_probs = self.time_in_states / total_t
        else:
            curr_probs = np.zeros(3)
        
        self.conv_steps.append(self.steps_done)
        for i in range(3):
            self.conv_probs[i].append(curr_probs[i])

        self.current_state = next_state

        max_err = np.max(np.abs(curr_probs - self.pi_theoretical))
        self.status_var.set(f"Прошло дней: {self.current_time:.2f} | Max Err: {max_err:.4f}")
        
        self.draw_plots()
  
        delay = int(self.var_speed.get())
        self.root.after(delay, self.update_simulation_step)

    def draw_plots(self):
        if self.history_times:
            self.line_state.set_data(self.history_times, self.history_states)
            min_t = min(self.history_times)
            max_t = max(self.history_times)
            padding = (max_t - min_t) * 0.05 if max_t > min_t else 1
            self.ax1.set_xlim(min_t - padding, max_t + padding)
            self.ax1.set_ylim(-0.5, 2.5)

        if self.conv_steps:
            for i in range(3):
                self.lines_conv[i].set_data(self.conv_steps, self.conv_probs[i])
            max_step = max(self.conv_steps) * 1.05 if self.conv_steps else 10
            self.ax2.set_xlim(0, max_step)
            self.ax2.set_ylim(0, 1)  

        self.canvas.draw_idle()

    def start_sim(self):
        if self.simulation_finished:
            self.reset_sim()
        
        try:
            self.total_steps = int(self.var_n.get())
        except:
            messagebox.showerror("Ошибка", "Некорректное число шагов N")
            return

        if not self.is_running:
            self.is_running = True
            self.simulation_finished = False
            self.root.after(0, self.update_simulation_step)
            
    def reset_sim(self):
        self.is_running = False
        self.simulation_finished = False

        self.current_state = int(np.argmax(self.pi_theoretical))
        self.current_time = 0.0
        self.steps_done = 0
        self.time_in_states = np.zeros(3)
        self.log_data = []
        
        self.history_states = []
        self.history_times = []
        self.conv_steps = []
        self.conv_probs = [[] for _ in range(3)]
        
        self.status_var.set("Прошло дней: 0.00 | Ошибка: 0.0000")
        
        self.line_state.set_data([], [])
        for l in self.lines_conv:
            l.set_data([], [])
        self.error_text.set_text('')
        
        self.ax1.set_xlim(0, 10)
        self.ax1.set_ylim(-0.5, 2.5)
        self.ax2.set_xlim(0, 10)
        self.ax2.set_ylim(0, 1)
        
        self.canvas.draw_idle()

    def stop_sim(self):
        self.is_running = False

    def save_csv(self):
        if self.steps_done == 0:
            messagebox.showwarning("Внимание", "Нет данных для сохранения. Запустите симуляцию.")
            return

        total_t = np.sum(self.time_in_states)
        pi_empirical = self.time_in_states / total_t if total_t > 0 else np.zeros(3)
        
        abs_err = np.abs(pi_empirical - self.pi_theoretical)

        df_summary = pd.DataFrame({
            'Состояние': STATE_NAMES,
            'Теоретическая_доля': np.round(self.pi_theoretical, 6),
            'Эмпирическая_доля': np.round(pi_empirical, 6),
            'Абсолютная_ошибка': np.round(abs_err, 6),
            'Время_в_состоянии_дней': np.round(self.time_in_states, 4)
        })
        df_traj = pd.DataFrame(self.log_data)
        filename = filedialog.asksaveasfilename(defaultextension=".csv", filetypes=[("CSV files", "*.csv")])
        if filename:
            base_name = filename.replace('.csv', '')
            df_summary.to_csv(f"{base_name}_summary.csv", index=False, encoding='utf-8-sig')
            df_traj.to_csv(f"{base_name}_trajectory.csv", index=False, encoding='utf-8-sig')
            messagebox.showinfo("Успех", f"Файлы сохранены:\n1. {base_name}_summary.csv (Статистика)\n2. {base_name}_trajectory.csv (Лог)")

if __name__ == "__main__":
    root = tk.Tk()
    app = MarkovWeatherApp(root)
    root.mainloop()