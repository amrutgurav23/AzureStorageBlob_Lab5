using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;


//Storage Account name- "azureblobstoragedemo1"
//Container Name- "mycontainer123"
//Storage Account connection string- "DefaultEndpointsProtocol=https;AccountName=azureblobstoragedemo1;AccountKey=NdG6fYcZ12PTHAvRU7mkQf4YNm+kcq2G9cbJ4rQImdoJs0YEy5JzuSn05+RTD8dq8L+vvh0VCbGc+ASt7H9a4g==;EndpointSuffix=core.windows.net"


// Configure the connection string 
string connectionString = "DefaultEndpointsProtocol=https;AccountName=azureblobstoragedemo1;AccountKey=NdG6fYcZ12PTHAvRU7mkQf4YNm+kcq2G9cbJ4rQImdoJs0YEy5JzuSn05+RTD8dq8L+vvh0VCbGc+ASt7H9a4g==;EndpointSuffix=core.windows.net";
string localFilePath = @"D:\Amrut_Study\Azure\UploadFiles\samplevideo.mp4";

#region BlockBlob-Default Upload type
////Reference URL- "https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet"
//// Create a BlobServiceClient object -to access storage
//var blobServiceClient = new BlobServiceClient(connectionString);

////BlobContainerClient class allows you to manipulate Azure Storage containers and their blobs
//BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mycontainer123");

//// Get a reference to a blob
//BlobClient blobClient = containerClient.GetBlobClient("mp4");

//Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

//// Upload a blob to a container from the local file
//string localFilePath = @"D:\Amrut_Study\Azure\UploadFiles\samplevideo.mp4";
//await blobClient.UploadAsync(localFilePath, true);
#endregion

#region BlockBlob-Configured
//Reference Url https://learn.microsoft.com/en-us/dotnet/api/microsoft.azure.storage.blob.cloudblockblob.uploadfromfileasync?view=azure-dotnet-legacy
CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
blobClient.DefaultRequestOptions = new BlobRequestOptions() { 
SingleBlobUploadThresholdInBytes = 1024 * 1024,//1 MB, the minimum
ParallelOperationThreadCount = 3
};

CloudBlobContainer container = blobClient.GetContainerReference("mycontainer123");
CloudBlockBlob blockBlob = container.GetBlockBlobReference("mp4chunk");
blockBlob.StreamWriteSizeInBytes = 1024 * 1024;
await blockBlob.UploadFromFileAsync(localFilePath);
#endregion
Console.WriteLine("Hello, World!");
Console.ReadLine();
