#!/usr/bin/env -S dotnet fsi
#r "nuget: Fun.Build, 1.1.5"
#r "nuget: dotenv.net, 3.2.0"

open System.IO
open Fun.Build

[<RequireQualifiedAccess>]
module Stages =
  let restore = stage "tool-restore" { run "dotnet tool restore" }

  let run = stage "run" { run "dotnet run --project src/Website " }
  let watch = stage "watch" { run "dotnet watch run --project src/Website" }
  let serve = stage "serve" { run "dotnet serve -d dist -p 8000 -q" }

pipeline "watch" {
  Stages.restore
  stage "watch & serve in parallel" {
    Directory.CreateDirectory("dist") |> ignore // Required so dotnet serve doesn't complain
    Stages.watch
    Stages.serve
    paralle
  }

  runIfOnlySpecified true
}
