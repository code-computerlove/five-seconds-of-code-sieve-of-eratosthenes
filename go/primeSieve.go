package main

type primeSieve []bool

func getSieve(sieveSize int) primeSieve {
	sieve := make([]bool, sieveSize+1)
	for i := range sieve {
		sieve[i] = true
	}
	return sieve
}

func (sieve primeSieve) run() {
	for factor := 2; factor < len(sieve); factor++ {
		if sieve[factor] {
			q := factor + factor
			for q < len(sieve) {
				sieve[q] = false
				q += factor
			}
		}
	}
}

func (sieve primeSieve) result() []int {
	current := 2
	var primes []int

	for _, isPrime := range sieve[2:] {
		if isPrime {
			primes = append(primes, current)
		}
		current++
	}

	return primes
}
