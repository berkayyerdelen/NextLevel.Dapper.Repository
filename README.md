# NextLevel.Dapper.Repository
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


