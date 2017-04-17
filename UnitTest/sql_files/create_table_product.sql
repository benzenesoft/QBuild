CREATE TABLE product(
   id INT PRIMARY KEY     NOT NULL,
   name           TEXT    NOT NULL,
   price          INT     NOT NULL
);

INSERT INTO product (id, name, price)
	VALUES 
		(1, "almira",  1000),
		(2, "almira",   500),
		(3,  "table",   500),
		(4, "zipper",     5),
		(5, "zipper",    10)