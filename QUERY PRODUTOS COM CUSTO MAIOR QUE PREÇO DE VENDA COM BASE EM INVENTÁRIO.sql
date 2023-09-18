SELECT 
	codproduto, 
	produto, 
	produtos.unidade, 
	produtos.codncm, 	
	grupos.nome,
	inventario_produtos.custounitario::money,
	produtos.precovenda::money 
FROM inventario_produtos 
LEFT JOIN produtos ON (produtos.codigo = inventario_produtos.codproduto)
LEFT JOIN grupos ON (grupos.codigo = inventario_produtos.codgrupo)
WHERE codinventario = 434 AND inventario_produtos.custounitario > produtos.precovenda