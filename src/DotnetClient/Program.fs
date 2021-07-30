open Fable.Remoting.DotnetClient
open Shared

let todosApi =
    Remoting.createApi "http://localhost:8085"
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let printTodos () = async {
    let! todos = todosApi.getTodos ()

    let mutable itemNo = 1
    for todo in todos do
        printfn "Todo %i: %s" itemNo todo.Description
        itemNo <- itemNo + 1
}

[<EntryPoint>]
let main _ =
    let mutable shouldContinue = true
    while shouldContinue do
        printTodos () |> Async.RunSynchronously
        
        printfn "%sPress 'q' to exit or any other key to fetch and print the latest todos.%s"
            System.Environment.NewLine System.Environment.NewLine
        let k = System.Console.ReadKey()
        shouldContinue <- k.KeyChar <> 'q'
    0