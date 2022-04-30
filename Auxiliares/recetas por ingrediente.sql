
select R.NOmbre From Recetas r
inner join IngredientesRecetas ir on r.Id = ir.RecetaId
inner join Ingredientes i on i.Id = ir.IngredienteId
where i.descripcion like '%zapallo%'

select R.NOmbre From Recetas r
inner join IngredientesRecetas ir on r.Id = ir.RecetaId
inner join Ingredientes i on i.Id = ir.IngredienteId
where i.descripcion like '%lechuga%'

select R.NOmbre From Recetas r
inner join IngredientesRecetas ir on r.Id = ir.RecetaId
inner join Ingredientes i on i.Id = ir.IngredienteId
where i.descripcion like '%pollo%'