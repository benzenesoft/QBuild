CREATE TABLE product(
   id 		INT		NOT NULL PRIMARY KEY
   ,name    TEXT	NOT NULL
   ,price   INT		NOT NULL
   ,size	TEXT	NOT NULL
   ,color	TEXT	NOT NULL
);

INSERT INTO product (id, name, price, size, color)
	VALUES 
		 ( 1	,"almira"	,80		,"l"	,"black"	)
		,( 2	,"almira"	,70		,"m"	,"red"      )
		,( 3	,"bed"		,60		,"l"	,"red"      )
		,( 4	,"bed"		,50		,"m"	,"red"      )
		,( 5	,"bench"	,15		,"s"	,"white"    )
		,( 6	,"chair"	,30		,"l"	,"green"    )
		,( 7	,"chair"	,25		,"m"	,"white"    )
		,( 8	,"chair"	,25		,"m"	,"black"    )
		,( 9	,"chair"	,20		,"m"	,"red"      )
		,(10	,"chair"	,20		,"m"	,"blue"     )
		,(11	,"desk"		,25		,"m"	,"yellow"   )
		,(12	,"sofa"		,65		,"l"	,"red"      )
		,(13	,"sofa"		,45		,"m"	,"blue"     )
		,(14	,"table"	,55		,"m"	,"blue"     )
		,(15	,"table"	,40		,"s"	,"green"    );