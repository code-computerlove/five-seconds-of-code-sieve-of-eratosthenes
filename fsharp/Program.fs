namespace DragRace

module main =
    open System.Diagnostics

    let validationData =
        [ 10, 4 // Historical data for validating our results - the number of primes
          100, 25 // to be found under some limit, such as 168 primes under 1000
          1_000, 168
          10_000, 1229
          100_000, 9592
          1_000_000, 78498
          10_000_000, 664579
          100_000_000, 5761455 ]
        |> Map.ofList

    module Sieve =
        let sieveOut factor candidates =
            let rec markNonPrimes factor q =
                if q > Array.length candidates then
                    candidates
                else
                    candidates.[q - 1] <- false
                    markNonPrimes factor (q + factor)

            markNonPrimes factor (factor + factor)

        let run candidates =
            let rec run' factor candidates =
                if factor > Array.length candidates then candidates
                else if candidates.[factor - 1] then run' (factor + 1) (sieveOut factor candidates)
                else run' (factor + 1) candidates

            run' 2 candidates

        let getResults candidates =
            let rec getResults' candidates current primes =
                if current > Array.length candidates
                then primes
                else if candidates.[current - 1]
                then getResults' candidates (current + 1) (current :: primes)
                else getResults' candidates (current + 1) primes

            getResults' candidates 2 [] |> List.rev

    let sieveSize = 1_000_000

    [<EntryPoint>]
    let main _ =
        let stopwatch = Stopwatch.StartNew()

        let rec go laps =
            if stopwatch.Elapsed.Seconds > 5 then
                stopwatch.Stop()
                laps
            else
                Array.init sieveSize (fun _ -> true)
                |> Sieve.run
                |> ignore

                go (laps + 1)

        let laps = go 0

        let primes =
            Array.init sieveSize (fun _ -> true)
            |> Sieve.run
            |> Sieve.getResults

        let isCorrectNumberOfPrimes = List.length primes = validationData.[sieveSize]

        printfn "Laps: %d Time: %f #Primes: %d Valid: %b" laps stopwatch.Elapsed.TotalSeconds (List.length primes)
            isCorrectNumberOfPrimes
        0
