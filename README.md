# StorageQueueDemo

## What it does

This program fires up two processes:

### Sender

The sender console app generates trivial text messages that look like this:

 > LYCHEN1041-63
 > Time and date: Mon Jun 2017 ... 10:41
 > Message:surveillanceeventually
 > End!
 
(The message is two random words from a large corpus)

These messages are submitted to a StorageQueue. The program then takes a random-length rest for up to a second.

### Receiver

The receiver checks the StorageQueue from above. It checks a message out, and processes it to turn the top line into a file name and extract the message text. This is just to do some arbitrary processing, you could equally be receiving full-size JPEG photos in the queue and process them by creating thumbnails.

The processed data is then written to a file in a storage blob: i.e. `LYCHEN1041-63.txt` containing `surveillanceeventually`

## Scalability

You can run as many sender and receiver processes as you want. The senders get a unique name based on machine and time started, so they don't create identical message file names. If the single receiver was overwhelmed, you could start more instances of the receiver process.

I need to re-tool the programs to run as cloud services to show how this can scale in the cloud too.

## Setup

Open the `program.cs` file for Sender and Receiver, and put your StorageAccount connection string in the `connectionString` variable. I should move that to app.config...

You can find your key in the Azure Portal; go to or create a Storage Account, then under Settings, click 'Access keys' and use either of the connection strings from there.