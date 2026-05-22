import numpy as np
import matplotlib.pyplot as plt
import csv
import tkinter as tk
from tkinter import ttk, messagebox
from collections import deque
import threading

# 1. ЛОГИКА СИМУЛЯЦИИ 

def get_random_interval(dist_type, rate=None, params=None):
    if dist_type == 'exp':
        if rate is None or rate <= 0: return float('inf')
        u = np.random.uniform(0, 1)
        return -np.log(u) / rate
    elif dist_type == 'norm':
        val = np.random.normal(loc=params['loc'], scale=params['scale'])
        return max(0.001, val)
    elif dist_type == 'unif':
        return np.random.uniform(params['low'], params['high'])
    else:
        raise ValueError(f"Unknown dist: {dist_type}")

def run_simulation_core(params):
    LAMBDA = params['lambda']
    MU = params['mu']
    N_SERVERS = params['n_servers']
    T_MODEL = params['t_model']
    DIST_ARRIVAL = params['dist_arrival']
    DIST_SERVICE = params['dist_service']
    
    # Параметры для не-exp распределений (упрощенно берем среднее 1/lambda)
    arr_params = {'loc': 1/LAMBDA if LAMBDA > 0 else 1, 'scale': 0.5, 'low': 0, 'high': 2/LAMBDA if LAMBDA > 0 else 2}
    srv_params = {'loc': 1/MU if MU > 0 else 1, 'scale': 0.2, 'low': 0, 'high': 2/MU if MU > 0 else 2}

    t = 0.0
    n_busy = 0
    queue = deque()
    
    ta = t + get_random_interval(DIST_ARRIVAL, rate=LAMBDA, params=arr_params)
    ts = [float('inf')] * N_SERVERS
    
    stats_w_queue = []
    stats_L_samples = []
    detailed_log = []
    clients_processed = 0
    
    while t < T_MODEL:
        min_ts = min(ts)
        
        # Определение следующего события
        if ta <= min_ts and ta < T_MODEL:
            t_next = ta
            event_type = 'arrival'
        elif min_ts < T_MODEL:
            t_next = min_ts
            event_type = 'departure'
            server_idx = ts.index(min_ts)
        else:
            break
            
        t = t_next
        current_L = n_busy + len(queue)
        stats_L_samples.append(current_L)

        if event_type == 'arrival':
            ta = t + get_random_interval(DIST_ARRIVAL, rate=LAMBDA, params=arr_params)
            
            # Быстренько занимаем оператора
            if n_busy < N_SERVERS:
                free_idx = ts.index(float('inf'))
                service_time = get_random_interval(DIST_SERVICE, rate=MU, params=srv_params)
                ts[free_idx] = t + service_time
                n_busy += 1
            else:
                queue.append(t)
                
        elif event_type == 'departure':
            ts[server_idx] = float('inf')
            n_busy -= 1
            
            if queue:
                t_arrival_client = queue.popleft()
                t_start_service = t
                n_busy += 1
                
                service_time = get_random_interval(DIST_SERVICE, rate=MU, params=srv_params)
                ts[server_idx] = t + service_time
                t_end_service = ts[server_idx]
                
                w_q = t_start_service - t_arrival_client
                w_s = t_end_service - t_arrival_client
                
                stats_w_queue.append(w_q)
                
                detailed_log.append({
                    'id': clients_processed + 1,
                    't_arr': t_arrival_client,
                    't_start': t_start_service,
                    't_end': t_end_service,
                    'w_queue': w_q,
                    'w_system': w_s
                })
                clients_processed += 1

    # Расчет итоговых метрик
    results = {
        'avg_w_queue': np.mean(stats_w_queue) if stats_w_queue else 0,
        'avg_L': np.mean(stats_L_samples) if stats_L_samples else 0,
        'clients_processed': clients_processed,
        'L_samples': stats_L_samples,
        'W_queue_samples': stats_w_queue,
        'log': detailed_log
    }
    return results

# 2. GUI 

class SimulationApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Лабораторная №9: СМО (Пункт выдачи)")
        self.root.geometry("400x350")
        
        input_frame = ttk.LabelFrame(root, text="Параметры модели", padding=10)
        input_frame.pack(fill="x", padx=10, pady=10)
        
        ttk.Label(input_frame, text="Lambda (интенс. прибытия):").grid(row=0, column=0, sticky="w", pady=2)
        self.entry_lambda = ttk.Entry(input_frame)
        self.entry_lambda.insert(0, "2.0")
        self.entry_lambda.grid(row=0, column=1, sticky="ew", pady=2)
        
        ttk.Label(input_frame, text="Mu (интенс. обслуж.):").grid(row=1, column=0, sticky="w", pady=2)
        self.entry_mu = ttk.Entry(input_frame)
        self.entry_mu.insert(0, "1.0")
        self.entry_mu.grid(row=1, column=1, sticky="ew", pady=2)

        ttk.Label(input_frame, text="N (кол-во операторов):").grid(row=2, column=0, sticky="w", pady=2)
        self.entry_n = ttk.Entry(input_frame)
        self.entry_n.insert(0, "3")
        self.entry_n.grid(row=2, column=1, sticky="ew", pady=2)

        ttk.Label(input_frame, text="T model (время):").grid(row=3, column=0, sticky="w", pady=2)
        self.entry_t = ttk.Entry(input_frame)
        self.entry_t.insert(0, "500")
        self.entry_t.grid(row=3, column=1, sticky="ew", pady=2)
        
        ttk.Label(input_frame, text="Распр. прибытия:").grid(row=4, column=0, sticky="w", pady=2)
        self.combo_dist_arr = ttk.Combobox(input_frame, values=["exp", "norm", "unif"], state="readonly")
        self.combo_dist_arr.set("exp")
        self.combo_dist_arr.grid(row=4, column=1, sticky="ew", pady=2)
        
        ttk.Label(input_frame, text="Распр. обслуж.:").grid(row=5, column=0, sticky="w", pady=2)
        self.combo_dist_srv = ttk.Combobox(input_frame, values=["exp", "norm", "unif"], state="readonly")
        self.combo_dist_srv.set("exp")
        self.combo_dist_srv.grid(row=5, column=1, sticky="ew", pady=2)

        btn_frame = ttk.Frame(root)
        btn_frame.pack(fill="x", padx=10, pady=10)
        
        self.btn_run = ttk.Button(btn_frame, text="Запустить симуляцию", command=self.start_simulation)
        self.btn_run.pack(fill="x")
        
        self.label_status = ttk.Label(root, text="Готов к запуску", foreground="gray")
        self.label_status.pack(pady=5)

    def start_simulation(self):
        try:
            params = {
                'lambda': float(self.entry_lambda.get()),
                'mu': float(self.entry_mu.get()),
                'n_servers': int(self.entry_n.get()),
                't_model': float(self.entry_t.get()),
                'dist_arrival': self.combo_dist_arr.get(),
                'dist_service': self.combo_dist_srv.get()
            }
            if params['lambda'] <= 0 or params['mu'] <= 0 or params['n_servers'] < 1:
                raise ValueError("Lambda, Mu > 0; N >= 1")
        except Exception as e:
            messagebox.showerror("Ошибка ввода", f"Проверьте параметры:\n{e}")
            return

        self.btn_run.config(state="disabled")
        self.label_status.config(text="Моделирование...", foreground="blue")
        self.root.update()
        
        thread = threading.Thread(target=self.run_worker, args=(params,))
        thread.start()

    def run_worker(self, params):
        try:
            results = run_simulation_core(params)
            self.plot_results(results)
            self.root.after(0, lambda: self.label_status.config(text="Готово!", foreground="green"))
        except Exception as e:
            self.root.after(0, lambda: messagebox.showerror("Ошибка симуляции", str(e)))
        finally:
            self.root.after(0, lambda: self.btn_run.config(state="normal"))

    def plot_results(self, results):
        l_samples = results['L_samples']
        w_queue_samples = results['W_queue_samples']
        
        if not w_queue_samples:
            messagebox.showwarning("Предупреждение", "Недостаточно данных для графиков (слишком короткое время(или меньше нуля?)")
            return

        fig, axes = plt.subplots(1, 2, figsize=(12, 5))
        
        # 1. Полигон L
        unique_L, counts_L = np.unique(l_samples, return_counts=True)
        probs_L = counts_L / np.sum(counts_L)
        axes[0].plot(unique_L, probs_L, marker='o', linestyle='-', color='b')
        axes[0].set_title(f'Распределение L (Среднее: {results["avg_L"]:.2f})')
        axes[0].set_xlabel('Число заявок')
        axes[0].set_ylabel('Вероятность')
        axes[0].grid(True)

        # 2. Гистограмма Wq
        axes[1].hist(w_queue_samples, bins=30, color='g', alpha=0.7, edgecolor='black', density=True)
        axes[1].axvline(results['avg_w_queue'], color='r', linestyle='--', label=f'Среднее Wq: {results["avg_w_queue"]:.2f}')
        axes[1].set_title('Время ожидания в очереди')
        axes[1].set_xlabel('Время')
        axes[1].set_ylabel('Плотность')
        axes[1].legend()
        axes[1].grid(True)
        
        plt.tight_layout()
        plt.savefig("lab9_gui_plots.png")
        plt.show()

# 3. ЗАПУСК

if __name__ == "__main__":
    root = tk.Tk()
    app = SimulationApp(root)
    root.mainloop()