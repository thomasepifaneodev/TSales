------------------------------------------------
CREATE OR REPLACE FUNCTION public.insertuserpg(
    login text,
    pass text)
RETURNS character varying AS
$BODY$
DECLARE 
    username TEXT := login;
    senha TEXT := pass;
BEGIN
    EXECUTE 'CREATE USER ' || quote_ident(username) || ' WITH ENCRYPTED PASSWORD ' || quote_literal(senha);
    RETURN 'Success';
END 
$BODY$
LANGUAGE plpgsql VOLATILE
COST 100;

ALTER FUNCTION public.insertuserpg(text, text)
OWNER TO postgres;
------------------------------------------------
CREATE OR REPLACE FUNCTION public.pg_grant(
    usuario text,
    permissoes text,    
    esquema text)
  RETURNS integer AS
$BODY$
DECLARE
    obj RECORD;
    num INTEGER;
BEGIN
    num := 0;
    FOR obj IN SELECT
                   relname
                   FROM
                       pg_class c
                           JOIN pg_namespace ns ON (c.relnamespace = ns.oid)
                   WHERE
                       relkind IN ('r', 'v', 'S', 'm')
                       AND nspname = 'public'
        LOOP
            EXECUTE 'GRANT ' || permissoes || ' ON ' || obj.relname || ' TO ' || usuario;
            num := num + 1;
        END LOOP;
    RETURN num;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public.pg_grant(text, text, text)
  OWNER TO postgres;
------------------------------------------------
CREATE OR REPLACE FUNCTION public.pg_grantall()
  RETURNS boolean AS
$BODY$
DECLARE
	TAB RECORD;
BEGIN	
	BEGIN
	
		FOR TAB IN SELECT LOGIN FROM USUARIOS LOOP
			IF (SELECT USENAME FROM PG_USER WHERE USENAME = TAB.LOGIN) <> COALESCE('') THEN		
				RAISE NOTICE 'Definindo permissões para o usuário %', TAB.LOGIN;
				PERFORM PG_GRANT(TAB.LOGIN, 'SELECT, INSERT, UPDATE, DELETE','%','public');
			END IF;		
		END LOOP;
	
	EXCEPTION	
		WHEN OTHERS THEN 
			FOR TAB IN SELECT USENAME FROM PG_USER LOOP
				PERFORM PG_GRANT(TAB.USENAME, 'SELECT, INSERT, UPDATE, DELETE','%','public');
			END LOOP;			
	END;
	RETURN TRUE;	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public.pg_grantall()
  OWNER TO postgres;
------------------------------------------------
CREATE OR REPLACE FUNCTION public.pg_revoke(
    usuario text,
    permissoes text,    
    esquema text)
  RETURNS integer AS
$BODY$
DECLARE
    obj RECORD;
    num INTEGER;
BEGIN
    num := 0;
    FOR obj IN SELECT
                   relname
                   FROM
                       pg_class c
                           JOIN pg_namespace ns ON (c.relnamespace = ns.oid)
                   WHERE
                       relkind IN ('r', 'v', 'S', 'm')
                       AND nspname = 'public'
        LOOP
            EXECUTE 'REVOKE ' || permissoes || ' ON ' || obj.relname || ' FROM ' || usuario;
            num := num + 1;
        END LOOP;
    RETURN num;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public.pg_revoke(text, text, text)
  OWNER TO postgres;
------------------------------------------------
CREATE OR REPLACE FUNCTION public.insertuserpg(
    login text,
    pass text)
  RETURNS character varying AS
$BODY$
DECLARE 
    username TEXT := login;
    senha TEXT := pass;
BEGIN
    EXECUTE 'CREATE USER ' || quote_ident(username) || ' WITH ENCRYPTED PASSWORD ' || quote_literal(senha);
    RETURN 'Success';
END 
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public.insertuserpg(text, text)
  OWNER TO postgres;
------------------------------------------------
CREATE SEQUENCE public.users_codigo
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
ALTER TABLE public.users_codigo
  OWNER TO postgres;
------------------------------------------------
CREATE SEQUENCE public.clientes_codigo
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
ALTER TABLE public.clientes_codigo
  OWNER TO postgres;
------------------------------------------------
CREATE SEQUENCE public.produtos_codigo
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
ALTER TABLE public.produtos_codigo
  OWNER TO postgres;
------------------------------------------------
CREATE TABLE public.clientes(
codigo INTEGER NOT NULL DEFAULT nextval('clientes_codigo'::regclass),
nome CHARACTER VARYING(60) NOT NULL,
cpfcnpj CHARACTER VARYING(18),
cidade CHARACTER VARYING(50),
telefone CHARACTER VARYING(14),
email CHARACTER VARYING(60),
cr CHARACTER VARYING(15), 
CONSTRAINT clientes_pkey PRIMARY KEY (codigo))
------------------------------------------------
CREATE TABLE public.produtos
(
  codigo integer NOT NULL DEFAULT nextval('produtos_codigo'::regclass),
  descricao character varying(60) NOT NULL,
  custounitario numeric(13, 2),
  precovenda numeric(13, 2),
  grupo character varying(20),
  unidade character varying(15),
  margemlucro numeric(13, 2),
  estoque numeric(13, 2),
  codean character varying(14),
  CONSTRAINT produtos_pkey PRIMARY KEY (codigo)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.produtos
  OWNER TO postgres;
------------------------------------------------
CREATE TABLE public.users
(
  codigo integer NOT NULL DEFAULT nextval('users_codigo'::regclass),
  login character varying(20) NOT NULL,    
  pass character varying(44),
  adm boolean,
  CONSTRAINT users_pkey PRIMARY KEY (codigo)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.users
  OWNER TO postgres;
