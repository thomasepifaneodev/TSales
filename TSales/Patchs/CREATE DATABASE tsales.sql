CREATE FUNCTION public.insertuserpg(login text, pass text) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
DECLARE 
    username TEXT := login;
    senha TEXT := pass;
BEGIN
    EXECUTE 'CREATE USER ' || quote_ident(username) || ' WITH ENCRYPTED PASSWORD ' || quote_literal(senha);
    RETURN 'Success';
END 
$$;
--
ALTER FUNCTION public.insertuserpg(login text, pass text) OWNER TO postgres;
--
CREATE FUNCTION public.insertuserpgsuper(login text, pass text) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
DECLARE 
    username TEXT := login;
    senha TEXT := pass;
BEGIN
    EXECUTE 'CREATE USER ' || quote_ident(username) || ' SUPERUSER ENCRYPTED PASSWORD ' || quote_literal(senha);
    RETURN 'Success';
END 
$$;
--
ALTER FUNCTION public.insertuserpgsuper(login text, pass text) OWNER TO postgres;
--
CREATE FUNCTION public.pg_grant(usuario text, permissoes text, esquema text) RETURNS integer
    LANGUAGE plpgsql
    AS $$
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
$$;
--
ALTER FUNCTION public.pg_grant(usuario text, permissoes text, esquema text) OWNER TO postgres;
--
CREATE FUNCTION public.pg_grantall() RETURNS boolean
    LANGUAGE plpgsql
    AS $$
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
$$;
--
ALTER FUNCTION public.pg_grantall() OWNER TO postgres;
--
CREATE FUNCTION public.pg_revoke(usuario text, permissoes text, esquema text) RETURNS integer
    LANGUAGE plpgsql
    AS $$
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
$$;
--
ALTER FUNCTION public.pg_revoke(usuario text, permissoes text, esquema text) OWNER TO postgres;
--
CREATE SEQUENCE public.clientes_codigo
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
ALTER TABLE public.clientes_codigo OWNER TO postgres;
--
CREATE TABLE public.clientes (
    codigo integer DEFAULT nextval('public.clientes_codigo'::regclass) NOT NULL,
    nome character varying(60) NOT NULL,
    cpfcnpj character varying(18),
    cidade character varying(50),
    telefone character varying(14),
    email character varying(60),
    cr character varying(15)
);
--
ALTER TABLE public.clientes OWNER TO postgres;
--
CREATE SEQUENCE public.produtos_codigo
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
ALTER TABLE public.produtos_codigo OWNER TO postgres;
--
CREATE TABLE public.produtos (
    codigo integer DEFAULT nextval('public.produtos_codigo'::regclass) NOT NULL,
    descricao character varying(60) NOT NULL,
    custounitario numeric(13,2) NOT NULL,
    precovenda numeric(13,2) NOT NULL,
    grupo character varying(20),
    unidade character varying(15) NOT NULL,
    margemlucro numeric(13,2),
    estoque numeric(13,2),
    codean character varying(14)
);
ALTER TABLE public.produtos OWNER TO postgres;
--
CREATE SEQUENCE public.users_codigo
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
ALTER TABLE public.users_codigo OWNER TO postgres;
--
CREATE TABLE public.users (
    codigo integer DEFAULT nextval('public.users_codigo'::regclass) NOT NULL,
    login character varying(20) NOT NULL,
    pass character varying(44),
    adm boolean
);
--
ALTER TABLE public.users OWNER TO postgres;
--
SELECT pg_catalog.setval('public.clientes_codigo', 1, true);
--
SELECT pg_catalog.setval('public.produtos_codigo', 1, true);
--
INSERT INTO public.users (codigo, login, pass, adm) VALUES (1, 'adm', 'c4ca4238a0b923820dcc509a6f75849b', true);
--
SELECT pg_catalog.setval('public.users_codigo', 2, false);
--
ALTER TABLE ONLY public.clientes
ADD CONSTRAINT clientes_pkey PRIMARY KEY (codigo);
--
ALTER TABLE ONLY public.produtos
ADD CONSTRAINT produtos_pkey PRIMARY KEY (codigo);
--
ALTER TABLE ONLY public.users
ADD CONSTRAINT users_pkey PRIMARY KEY (codigo);
--
CREATE UNIQUE INDEX users_login_unique ON public.users USING btree (login);
--
REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;
--
REVOKE ALL ON SEQUENCE public.clientes_codigo FROM PUBLIC;
REVOKE ALL ON SEQUENCE public.clientes_codigo FROM postgres;
GRANT ALL ON SEQUENCE public.clientes_codigo TO postgres;
GRANT ALL ON SEQUENCE public.clientes_codigo TO adm;
--
REVOKE ALL ON TABLE public.clientes FROM PUBLIC;
REVOKE ALL ON TABLE public.clientes FROM postgres;
GRANT ALL ON TABLE public.clientes TO postgres;
GRANT ALL ON TABLE public.clientes TO adm;
--
REVOKE ALL ON SEQUENCE public.produtos_codigo FROM PUBLIC;
REVOKE ALL ON SEQUENCE public.produtos_codigo FROM postgres;
GRANT ALL ON SEQUENCE public.produtos_codigo TO postgres;
GRANT ALL ON SEQUENCE public.produtos_codigo TO adm;
--
REVOKE ALL ON TABLE public.produtos FROM PUBLIC;
REVOKE ALL ON TABLE public.produtos FROM postgres;
GRANT ALL ON TABLE public.produtos TO postgres;
GRANT ALL ON TABLE public.produtos TO adm;
--
REVOKE ALL ON SEQUENCE public.users_codigo FROM PUBLIC;
REVOKE ALL ON SEQUENCE public.users_codigo FROM postgres;
GRANT ALL ON SEQUENCE public.users_codigo TO postgres;
GRANT ALL ON SEQUENCE public.users_codigo TO adm;
--
REVOKE ALL ON TABLE public.users FROM PUBLIC;
REVOKE ALL ON TABLE public.users FROM postgres;
GRANT ALL ON TABLE public.users TO postgres;
GRANT ALL ON TABLE public.users TO adm;