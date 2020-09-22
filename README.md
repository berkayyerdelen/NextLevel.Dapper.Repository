# NextLevel.Dapper.Repository
```csharp
var source= await productService.GetAllAsync("product", "*","name","WOW"); //Table Name  Fields , Where Clause, Parameter
```
```csharp
var source= await productService.GetAllAsync("product");
```
```csharp
var source= await productService.GetAllAsync("Table", "Id, Name");
```
```csharp
var source= await productService.GetByIdAsync("Product", "*", 1);
```
```csharp
var source= await productService.GetByIdAsync("product", 1);
```

```csharp
var source = await productService.IsInDbAsync("Product", 1);
var source = await productService.IsInDbAsync("Product","Name","Wow");
```

```csharp
 await productService.RemoveAsync("Product", "Name", "Wow");
 await productService.RemoveAsync("Product", 1);
 ```
```csharp
 await productService.UpdateAsync("Product", new Product() {Name = "AOM"});
 await productService.UpdateAsync("Product", new Product() {Name = "AOM"}, 1);
 ```

 ```csharp
 var source= await productService.ExecuteReadQuery("Select * from Product");
 ```

 ```csharp
 await productService.ExecuteWriteQuery("Delete from Products");
 ```
 ```csharp
 await productService.AddAsync("Product", new Product() {Name = "Dota"});
 ```


