# stats_utils.py
import math
from rng import MultiplicativeCongruentialGenerator

def generate_discrete_rv(rng: MultiplicativeCongruentialGenerator, values: list, probabilities: list) -> float:

    alpha = rng.next_double()
    cumulative_prob = 0.0
    
    for i, p in enumerate(probabilities):
        cumulative_prob += p
        if alpha < cumulative_prob:
            return values[i]
    
    return values[-1]

def generate_normal_rv_box_muller(rng: MultiplicativeCongruentialGenerator, mean: float = 0, std_dev: float = 1) -> float:

    u1 = rng.next_double()
    u2 = rng.next_double()
    
    if u1 == 0:
        u1 = 1e-10
        
    z0 = math.sqrt(-2.0 * math.log(u1)) * math.cos(2.0 * math.pi * u2)
    
    return mean + std_dev * z0

def calculate_mean_variance(data: list) -> tuple:
    n = len(data)
    if n == 0:
        return 0, 0
    
    mean = sum(data) / n
    variance = sum((x - mean) ** 2 for x in data) / n 
    return mean, variance

def chi_squared_test(data: list, values: list, probabilities: list, n_bins: int = None) -> dict:

    n = len(data)
    observed_freq = {}
    for v in values:
        observed_freq[v] = 0
        
    for x in data:
        if x in observed_freq:
            observed_freq[x] += 1
        else:
            pass 

    chi_sq_stat = 0
    degrees_of_freedom = len(values) - 1 
    
    for i, val in enumerate(values):
        n_obs = observed_freq[val]
        n_exp = n * probabilities[i]
        
        if n_exp > 0:
            chi_sq_stat += ((n_obs - n_exp) ** 2) / n_exp
            
    return {
        "statistic": chi_sq_stat,
        "df": degrees_of_freedom,
        "observed": observed_freq,
        "expected": {val: n * prob for val, prob in zip(values, probabilities)}
    }

def create_histogram_data(data: list, bins: int = 20) -> tuple:
    min_val = min(data)
    max_val = max(data)
    if min_val == max_val:
        return [min_val], [len(data)]
        
    step = (max_val - min_val) / bins
    histogram = {}
    
    bin_edges = []
    counts = []
    
    current_bin_start = min_val
    for i in range(bins):
        bin_end = current_bin_start + step
        count = sum(1 for x in data if current_bin_start <= x < bin_end)
        if i == bins - 1: 
             count = sum(1 for x in data if current_bin_start <= x <= bin_end)
             
        bin_edges.append(f"{current_bin_start:.2f}-{bin_end:.2f}")
        counts.append(count)
        current_bin_start = bin_end
        
    return bin_edges, counts