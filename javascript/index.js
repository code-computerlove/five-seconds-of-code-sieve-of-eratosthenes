#!/usr/bin/env node

const primeSieve = require("./primeSieve");

const validationData = {
	10: 4, // Historical data for validating our results - the number of primes
	100: 25, // to be found under some limit, such as 168 primes under 1000
	1_000: 168,
	10_000: 1229,
	100_000: 9592,
	1_000_000: 78498,
	10_000_000: 664579,
	100_000_000: 5761455,
};

const sieveSize = 1_000_000;
let laps = 0;
let sieve = null;

const startTime = new Date();

while (new Date() - startTime < 5000) {
	sieve = primeSieve.getSieve(sieveSize);
	sieve.run();
	laps++;
}

const endTime = new Date();

const primes = sieve.results();

const correctNumberOfPrimesFound = primes.length === validationData[sieveSize];

console.log(
	`Laps: ${laps} Time: ${(endTime - startTime) / 1000} #Primes: ${
		primes.length
	} Valid: ${correctNumberOfPrimesFound}`
);
