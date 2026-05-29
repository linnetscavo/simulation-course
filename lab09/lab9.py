import numpy as np
import tkinter as tk
from tkinter import ttk, messagebox
import threading

# 1. M/M/1 Система с отказами

def run_simulation_mm11(lambd, mu, T_model):
    server_free_time = 0.0  
    t_current = 0.0         
    
    n_accepted = 0
    n_refused = 0

    step = 100.0
    next_report_time = step
    report_text = ""
    
    u_arr = np.random.uniform(0, 1)
    dt_arrival = -np.log(u_arr) / lambd 
    
    while True:
        t_current += dt_arrival
        if t_current > T_model:
            break
        
        while t_current >= next_report_time and next_report_time <= T_model:
            total_interval = n_accepted + n_refused
            if total_interval > 0:
                p_acc = n_accepted / total_interval
                p_ref = n_refused / total_interval
            else:
                p_acc = 0.0
                p_ref = 0.0
                
            report_text += (f"[t={next_report_time:.0f}] Принято: {n_accepted}, "
                            f"Отказов: {n_refused} | "
                            f"P(прин): {p_acc:.3f}, P(отк): {p_ref:.3f}\n")
            
            next_report_time += step



        if t_current >= server_free_time:
            n_accepted += 1
            u_srv = np.random.uniform(0, 1)
            dt_service = -np.log(u_srv) / mu 
            server_free_time = t_current + dt_service
        else:
            n_refused += 1
            
        u_arr = np.random.uniform(0, 1)
        dt_arrival = -np.log(u_arr) / lambd 
    
    

    total = n_accepted + n_refused
    if total > 0:
        p_accept_final = n_accepted / total
        p_refuse_final = n_refused / total
    else:
        p_accept_final = 0.0
        p_refuse_final = 0.0

    report_text += (f"[t=1000] Принято: {n_accepted}, "
                            f"Отказов: {n_refused} | "
                            f"P(прин): {p_accept_final:.3f}, P(отк): {p_refuse_final:.3f}\n")
    return report_text

# 2. GUI

class SimulationApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Лабораторная №9: M/M/1/1 (По времени)")
        self.root.geometry("600x400")
        
        self.T_MODEL = 1000.0 

        control_frame = ttk.LabelFrame(root, text="Параметры модели", padding=10)
        control_frame.pack(fill="x", padx=10, pady=5)
        
        ttk.Label(control_frame, text="λ (прибытие):").grid(row=0, column=0, sticky="w", pady=5)
        self.entry_lambda = ttk.Entry(control_frame)
        self.entry_lambda.insert(0, "2.0")
        self.entry_lambda.grid(row=0, column=1, pady=5, padx=5)
        
        ttk.Label(control_frame, text="μ (обслуж.):").grid(row=0, column=2, sticky="w", pady=5)
        self.entry_mu = ttk.Entry(control_frame)
        self.entry_mu.insert(0, "3.0")
        self.entry_mu.grid(row=0, column=3, pady=5, padx=5)
        
        self.btn_run = ttk.Button(control_frame, text="Запустить", command=self.start_simulation)
        self.btn_run.grid(row=0, column=4, padx=10, sticky="e")
        
        res_frame = ttk.LabelFrame(root, text=f"Статистика (шаг 100 ед.)", padding=10)
        res_frame.pack(fill="both", expand=True, padx=10, pady=5)
        
        self.lbl_res = tk.Message(res_frame, text="Нажмите 'Запустить'...", 
                                  justify="left", font=("Consolas", 10), width=550)
        self.lbl_res.pack(anchor="w", fill="both", expand=True)

    def start_simulation(self):
        try:
            lambd = float(self.entry_lambda.get())
            mu = float(self.entry_mu.get())
            
            if lambd <= 0 or mu <= 0:
                raise ValueError("Параметры λ и μ должны быть > 0")
                
        except Exception as e:
            messagebox.showerror("Ошибка ввода", str(e))
            return
            
        self.btn_run.config(state="disabled")
        self.root.update()
        
        thread = threading.Thread(target=self.run_worker, args=(lambd, mu, self.T_MODEL))
        thread.daemon = True
        thread.start()
        
    def run_worker(self, lambd, mu, T_model):
        try:
            report_txt = run_simulation_mm11(lambd, mu, T_model)
            
            self.root.after(0, lambda: self.lbl_res.config(text=report_txt))
            self.root.after(0, lambda: self.btn_run.config(state="normal"))
            
        except Exception as e:
            self.root.after(0, lambda: messagebox.showerror("Ошибка", str(e)))
            self.root.after(0, lambda: self.btn_run.config(state="normal"))


if __name__ == "__main__":
    root = tk.Tk()
    app = SimulationApp(root)
    root.mainloop()