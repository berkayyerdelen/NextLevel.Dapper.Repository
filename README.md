# NextLevel.Dapper.Repository
### Where can I use it?
You can use it in every .Net application or library targeting one of the following profiles:
* .NET Standard 2
* .NET Core 2.1+
* .NET 4.5

You can also use it on previous version of these profiles but not fully tested.

### List of Repository Methods?
* GetAllAsync
* GetByIdAsync
* IsInDbAsync
* RemoveAsync
* UpdateAsync
* AddAsync
* ExecuteReadQuery
* ExecuteWriteQuery

### How to register service?
```csharp
 static IServiceProvider RegisterService()
        {
            var collection = new ServiceCollection();
            collection.AddDapperRepository(
                "ConnectionString");
            return collection.BuildServiceProvider();
        }
```        
### Usage
```csharp
var productService = service.GetService<IRepository<Product, int>>();
```
```csharp
var source= await productService.GetAllAsync("TableName");
```
```csharp
var source= await productService.GetAllAsync("TableName", "Fields");
```
```csharp
var source= await productService.GetAllAsync("TableName", "Fields","WhereClause","Param"); 
```
```csharp
var source= await productService.GetByIdAsync("TableName", "Fields", Id);
```
```csharp
var source= await productService.GetByIdAsync("TableName", 1);
```

```csharp
var source = await productService.IsInDbAsync("TableName", 1);
var source = await productService.IsInDbAsync("TableName","ColumnName","Param");
```

```csharp
await productService.RemoveAsync("TableName", Id);
await productService.RemoveAsync("TableName", "WhereClause", "Param");

 ```
```csharp
await productService.UpdateAsync("TableName", new Product() {Name = "AOM"});
await productService.UpdateAsync("TableName", new Product() {Name = "AOM"}, Id);
 ```

 ```csharp
 var source= await productService.ExecuteReadQuery("Select * from TableName");
 ```

 ```csharp
await productService.ExecuteWriteQuery("Delete from TableName");
 ```
 ```csharp
await productService.AddAsync("TableName", new Product() {Name = "Dota"});
 ```


