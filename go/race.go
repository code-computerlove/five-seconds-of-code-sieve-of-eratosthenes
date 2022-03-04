package main

import (
	"fmt"
	"time"
)

func main() {
	validationData := map[int]int{
		10:          4,  // Historical data for validating our results - the number of primes
		100:         25, // to be found under some limit, such as 168 primes under 1000
		1_000:       168,
		10_000:      1229,
		100_000:     9592,
		1_000_000:   78498,
		10_000_000:  664579,
		100_000_000: 5761455,
	}

	const sieveSize int = 1_000_000

	laps := 0

	var sieve primeSieve
	_ = sieve

	start := time.Now()
	for time.Since(start).Seconds() < 5 {
		sieve = getSieve(sieveSize)
		sieve.run()
		laps++
	}

	duration := time.Since(start).Seconds()

	primes := sieve.result()

	correctNumberOfPrimesFound := len(primes) == validationData[sieveSize]

	fmt.Printf("Laps: %d Time: %f #Primes: %d Valid: %t", laps, duration, len(primes), correctNumberOfPrimesFound)
}
