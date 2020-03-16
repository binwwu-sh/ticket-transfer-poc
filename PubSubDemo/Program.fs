open System
open FSharp.Control.Tasks
open Google.Cloud.PubSub.V1
open Google.Protobuf


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    let client = PublisherServiceApiClient.Create()

    let topicName = TopicName("anthos-dev-9438", "ticket-transfer-process")
    let resp =
        client.Publish(topicName, [
            PubsubMessage(Data = ByteString.CopyFromUtf8("test" + System.Random().Next().ToString()))
        ])

    let subName = SubscriptionName("anthos-dev-9438", "ticket-transfer-sub")
    let sub = SubscriberClient.CreateAsync(subName).Result
    sub.StartAsync(fun msg cancelToken ->
        task {
            printfn "Message received: %s" (msg.Data.ToStringUtf8())
            do! Async.Sleep 1000
            return SubscriberClient.Reply.Ack
        }
    )
    |> ignore

    printfn "%A" resp

    Console.ReadLine() |> ignore


    0 // return an integer exit code
