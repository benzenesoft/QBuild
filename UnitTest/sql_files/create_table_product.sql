CREATE TABLE product(
   id 				INT		NOT NULL PRIMARY KEY
   ,name			TEXT	NOT NULL
   ,price			INT		NOT NULL
   ,discounted_price INT	NOT NULL
   ,size			TEXT	NOT NULL
   ,color			TEXT	NOT NULL
   ,is_available	bool	NOT NULL
   ,comment			TEXT	NULL
);

INSERT INTO product (id, name, price, discounted_price, size, color, is_available)
	VALUES 
	--	  id	,name		,price	,dprice	,size	,color		,available	
		 ( 1	,"almira"	,80		,80		,"l"	,"black"	,0)
		,( 2	,"almira"	,70		,70		,"m"	,"red"      ,0)
		,( 3	,"bed"		,60		,60		,"l"	,"red"      ,1)
		,( 4	,"bed"		,50		,50		,"m"	,"red"      ,1)
		,( 5	,"bench"	,15		,15		,"s"	,"white"    ,1)
		,( 6	,"chair"	,30		,30		,"l"	,"green"    ,1)
		,( 7	,"chair"	,25		,25		,"m"	,"white"    ,1)
		,( 8	,"chair"	,25		,25		,"m"	,"black"    ,1)
		,( 9	,"chair"	,20		,20		,"m"	,"red"      ,1)
		,(10	,"chair"	,20		,20		,"m"	,"blue"     ,1)
		,(11	,"desk"		,25		,25		,"m"	,"yellow"   ,1)
		,(12	,"sofa"		,65		,60		,"l"	,"red"      ,1) -- has discount
		,(13	,"sofa"		,45		,40		,"m"	,"blue"     ,1) -- has discount
		,(14	,"table"	,55		,55		,"m"	,"blue"     ,1)
		,(15	,"table"	,40		,40		,"s"	,"green"    ,1);