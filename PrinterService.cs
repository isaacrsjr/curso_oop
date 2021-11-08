using System;
using System.Reactive.Subjects;

class PrinterServiceEventArgs
{
  public string DocumentId {get; private set;}
  public PrinterServiceEventArgs(string documentId)
     => DocumentId = documentId;
} 

class PrinterService
{
  public PrinterService(string name)
	=> Name = name;
	
  public readonly Subject<PrinterServiceEventArgs> BeforePrinting = new();
	
  public readonly Subject<PrinterServiceEventArgs> AfterPrinting = new();
	
  public string Name { get; private set; }
	
  public void Print(string documentId)
  {
	//if (BeforePrinting != null)
	//	BeforePrinting(new PrinterServiceNotification(documentId));
		
    BeforePrinting.OnNext(new PrinterServiceEventArgs(documentId));
	  
    Console.WriteLine($"Printing {documentId}...");	  
    AfterPrinting.OnNext(new PrinterServiceEventArgs(documentId));
  }
}


class PaperSupplierService
{
  public PaperSupplierService(PrinterService ps)
  {
	ps.BeforePrinting.Subscribe(PrinterService_BeforePrinting);
  }
   
  
  public void PrinterService_BeforePrinting(PrinterServiceEventArgs notification)
  {
  	Console.WriteLine($"PSS: {notification.DocumentId}");
  }
}

class TonerSupplierService
{
  public TonerSupplierService(PrinterService ps)
    => ps.BeforePrinting.Subscribe(PrinterService_BeforePrinting);
	
  public void PrinterService_BeforePrinting(PrinterServiceEventArgs notification)
    => Console.WriteLine($"TSS: {notification.DocumentId}");
}

class CleanerService
{
  public void Handle(PrinterService ps)
  {
	  ps.AfterPrinting.Subscribe(PrinterService_AfterPrinting);
  }
	
  public void PrinterService_AfterPrinting(PrinterServiceEventArgs notification)
  {
	Console.WriteLine($"CS: {notification.DocumentId}");
  }
}