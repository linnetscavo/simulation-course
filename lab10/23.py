import tkinter as tk
from tkinter import ttk, messagebox
import random
import matplotlib
matplotlib.use('TkAgg')
import matplotlib.pyplot as plt
from matplotlib.figure import Figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg

X_LIMIT = 100 


class Client:
    def __init__(self, arrival_time, service_duration):
        self.arrival_time = arrival_time      
        self.service_duration = service_duration 
        self.state = 'new'                    
        self.start_service_time = None       

class Server:
    def __init__(self, server_id):
        self.id = server_id
        self.is_busy = False
        self.free_at = 0.0                    
        self.current_client = None            

    def assign_client(self, client, current_time):
        self.is_busy = True
        self.current_client = client
        self.free_at = current_time + client.service_duration
        
        client.start_service_time = current_time
        client.state = 'serving'

    def release(self, current_time):
        self.is_busy = False
        finished_client = self.current_client
        self.current_client = None
        finished_client.state = 'done'
        return finished_client

class QueueManager:
    def __init__(self, max_size):
        self.max_size = max_size
        self.clients = []                     

    def add(self, client):
        if len(self.clients) < self.max_size:
            self.clients.append(client)
            client.state = 'waiting'
            return True
        return False                          

    def pop(self):
        if self.clients:
            return self.clients.pop(0)
        return None

    def is_full(self):
        return len(self.clients) >= self.max_size

    def is_empty(self):
        return len(self.clients) == 0
    
    def size(self):
        return len(self.clients)

class SMO_System:
    def __init__(self, lam, mu, n_servers, queue_size, T_model):
        self.lam = lam
        self.mu = mu
        self.T_model = T_model
        
        self.servers = [Server(i) for i in range(n_servers)]
        self.queue = QueueManager(queue_size)
        
        self.total_arrived = 0
        self.total_refused = 0
        self.total_served = 0
        
        # Для расчета среднего времени ожидания
        self.total_wait_time = 0.0 
        
        self.time_log = [0.0]
        self.queue_log = [0]

        self.next_arrival_time = random.expovariate(lam)

    def _get_next_departure_info(self):
        min_time = float('inf')
        active_server = None
        for server in self.servers:
            if server.is_busy and server.free_at < min_time:
                min_time = server.free_at
                active_server = server
        return min_time, active_server

    def run(self):
        t = 0.0
        
        while t < self.T_model:
            next_dep_time, dep_server = self._get_next_departure_info()
            
            if self.next_arrival_time <= next_dep_time:
                t = self.next_arrival_time
                self.total_arrived += 1
                
                service_dur = random.expovariate(self.mu)
                new_client = Client(t, service_dur)
                
                placed = False
                
                for server in self.servers:
                    if not server.is_busy:
                        server.assign_client(new_client, t)
                        placed = True
                        break
            
                if not placed:
                    if not self.queue.is_full():
                        self.queue.add(new_client)
                        placed = True
                    else:
                        self.total_refused += 1
                        new_client.state = 'refused'

                self.next_arrival_time = t + random.expovariate(self.lam)
                
            else:
                t = next_dep_time
                self.total_served += 1
                
                finished_client = dep_server.release(t)
                
                if finished_client.start_service_time is not None:
                    wait_time = finished_client.start_service_time - finished_client.arrival_time
                    self.total_wait_time += wait_time
                
                if not self.queue.is_empty():
                    next_client = self.queue.pop()
                    dep_server.assign_client(next_client, t)

            self.time_log.append(t)
            self.queue_log.append(self.queue.size())

        p_refuse = self.total_refused / self.total_arrived if self.total_arrived > 0 else 0
        p_acc = self.total_served / self.total_arrived if self.total_arrived > 0 else 0
        
        avg_wait = self.total_wait_time / self.total_served if self.total_served > 0 else 0
        
        return {
            'total': self.total_arrived,
            'n_accepted': self.total_served,
            'n_refused': self.total_refused,
            'p_acc': p_acc,
            'p_refuse': p_refuse,
            'avg_wait': avg_wait,     
            'time_log': self.time_log,
            'queue_log': self.queue_log
        }

# GUI ПРИЛОЖЕНИЕ

class SimulationApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Лабораторная: Агентное M/M/n + Queue")
        self.root.geometry("1000x650") 
        
        self.T_MODEL = 1000.0 
        
        top_frame = ttk.Frame(root)
        top_frame.pack(fill="x", padx=10, pady=10)

        control_frame = ttk.LabelFrame(top_frame, text="Параметры СМО", padding=10)
        control_frame.grid(row=0, column=0, sticky="nsew", padx=(0, 10))
        
        ttk.Label(control_frame, text="λ (вход):").grid(row=0, column=0, sticky="w")
        self.ent_lam = ttk.Entry(control_frame, width=8)
        self.ent_lam.insert(0, "5.0")
        self.ent_lam.grid(row=0, column=1, padx=5, pady=2)
        
        ttk.Label(control_frame, text="μ (обсл.):").grid(row=0, column=2, sticky="w")
        self.ent_mu = ttk.Entry(control_frame, width=8)
        self.ent_mu.insert(0, "2.0")
        self.ent_mu.grid(row=0, column=3, padx=5, pady=2)
        
        ttk.Label(control_frame, text="n (серверы):").grid(row=1, column=0, sticky="w")
        self.ent_n = ttk.Entry(control_frame, width=8)
        self.ent_n.insert(0, "2")
        self.ent_n.grid(row=1, column=1, padx=5, pady=2)
        
        ttk.Label(control_frame, text="Max очередь:").grid(row=1, column=2, sticky="w")
        self.ent_q = ttk.Entry(control_frame, width=8)
        self.ent_q.insert(0, "10")
        self.ent_q.grid(row=1, column=3, padx=5, pady=2)
        
        self.btn_run = ttk.Button(control_frame, text="Запустить симуляцию", command=self.run_sim)
        self.btn_run.grid(row=2, column=0, columnspan=4, pady=10, sticky="ew")

        res_frame = ttk.LabelFrame(top_frame, text="Результаты", padding=10)
        res_frame.grid(row=0, column=1, sticky="nsew", padx=(0, 0))
        
        self.lbl_res = ttk.Label(res_frame, text="Нажмите 'Запустить'", justify="left", font=("Consolas", 11))
        self.lbl_res.pack(anchor="w", fill="both", expand=True)

        top_frame.columnconfigure(0, weight=1)
        top_frame.columnconfigure(1, weight=1)

        plot_frame = ttk.LabelFrame(root, text=f"График очереди (Масштаб X: 0-{X_LIMIT})", padding=5)
        plot_frame.pack(fill="both", expand=True, padx=10, pady=5)

        self.fig = Figure(figsize=(8, 4), dpi=100)
        self.ax = self.fig.add_subplot(111)
        self.canvas = FigureCanvasTkAgg(self.fig, master=plot_frame)
        self.canvas.get_tk_widget().pack(fill="both", expand=True)

        self.last_res = None
        self.last_params = None

    def run_sim(self):
        try:
            lam = float(self.ent_lam.get())
            mu = float(self.ent_mu.get())
            n = int(self.ent_n.get())
            q = int(self.ent_q.get())
            
            if lam <= 0 or mu <= 0 or n <= 0 or q < 0: 
                raise ValueError
            
        except:
            messagebox.showerror("Ошибка", "Проверьте параметры (должны быть > 0)")
            return

        self.lbl_res.config(text="Считаю...")
        self.root.update()
        
        system = SMO_System(lam, mu, n, q, self.T_MODEL)
        res = system.run()
        
        self.last_res = res
        self.last_params = {'lam': lam, 'mu': mu, 'n': n, 'q': q}
        
        p_acc_pct = res['p_acc'] * 100
        p_ref_pct = res['p_refuse'] * 100
        
        # Добавляем среднее время ожидания в вывод
        txt = (f"Всего заявок:   {res['total']}\n"
               f"Обслужено:      {res['n_accepted']} ({p_acc_pct:.2f}%)\n"
               f"Отказов:        {res['n_refused']} ({p_ref_pct:.2f}%)\n"
               f"Ср. время ож.:  {res['avg_wait']:.4f} ед.\n"
               f"-------------------------\n")
        
        self.lbl_res.config(text=txt)
        self.draw_plot()

    def draw_plot(self):
        if not self.last_res:
            return
        
        time_log = self.last_res['time_log']
        queue_log = self.last_res['queue_log']
        
        self.ax.clear()
        self.ax.step(time_log, queue_log, where='post', label='Длина очереди', color='blue', linewidth=1.5)
        self.ax.axhline(y=self.last_params['q'], color='red', linestyle='--', label=f'Лимит ({self.last_params["q"]})')
        self.ax.set_xlim(0, X_LIMIT)
        self.ax.set_ylim(bottom=0)
        self.ax.set_title(f'Агентная модель M/M/{self.last_params["n"]}/{self.last_params["q"]}')
        self.ax.set_xlabel('Время (ед.)')
        self.ax.set_ylabel('Заявок в очереди')
        self.ax.legend(loc='upper right')
        self.ax.grid(True, alpha=0.3)
        self.canvas.draw()

if __name__ == "__main__":
    root = tk.Tk()
    app = SimulationApp(root)
    root.mainloop()