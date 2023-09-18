--
-- PostgreSQL database dump
--

-- Dumped from database version 9.5.25
-- Dumped by pg_dump version 9.5.25

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Data for Name: clientes; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.clientes (codigo, nome, cpfcnpj, cidade, telefone, email, cr) VALUES (3, 'THOMAS EPIFANEO DA SILVA OLIVEIRA', '056.560.405-85', 'CAMPO DO BRITO', '(79)9612-1016', 'THOMASEPIFANEODEV@GMAIL.COM', '123');
INSERT INTO public.clientes (codigo, nome, cpfcnpj, cidade, telefone, email, cr) VALUES (2, 'ELLEN ALINE TEIXEIRA PASSOS', '084.366.405-35', 'CAMPO DO BRITO', '(79)99843-7376', 'ELLENLSALINE@OUTLOOK.COM', '1234567890');


--
-- Name: clientes_codigo; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.clientes_codigo', 5, true);


--
-- Data for Name: pedidos; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.pedidos (codigo, cliente, qtdeitens, precototal, formapagto, usuario, datavenda) VALUES (1, 'THOMAS', 10.00, 2000.00, 'PIX', 'adm', '2023-09-13');
INSERT INTO public.pedidos (codigo, cliente, qtdeitens, precototal, formapagto, usuario, datavenda) VALUES (5, 'MATEUS ', 10.00, 540.00, 'PIX', 'adm', '2023-09-13');
INSERT INTO public.pedidos (codigo, cliente, qtdeitens, precototal, formapagto, usuario, datavenda) VALUES (6, 'IGOR', 10.00, 1000.00, 'PIX', 'adm', '2023-09-13');
INSERT INTO public.pedidos (codigo, cliente, qtdeitens, precototal, formapagto, usuario, datavenda) VALUES (2, 'JUNIOR', 10.00, 1350.00, 'CARTÃO', 'adm', '2023-09-13');
INSERT INTO public.pedidos (codigo, cliente, qtdeitens, precototal, formapagto, usuario, datavenda) VALUES (3, 'PEDRO', 10.00, 1900.00, 'DINHEIRO', 'adm', '2023-09-13');
INSERT INTO public.pedidos (codigo, cliente, qtdeitens, precototal, formapagto, usuario, datavenda) VALUES (4, 'MARCOS', 10.00, 900.00, 'BOLETO', 'adm', '2023-09-13');


--
-- Name: pedidos_codigo; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.pedidos_codigo', 1, false);


--
-- Data for Name: produtos; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.produtos (codigo, descricao, custounitario, precovenda, grupo, unidade, margemlucro, estoque, codean) VALUES (6, 'PISTOLA TS9 9MM TAURUS SA', 3500.00, 6500.00, 'PISTOLAS', 'PEÇA', 30.00, 10.00, '');
INSERT INTO public.produtos (codigo, descricao, custounitario, precovenda, grupo, unidade, margemlucro, estoque, codean) VALUES (3, 'PISTOLA COLT 1911A1 CAL. 45ACP', 9000.00, 13000.00, 'PISTOLAS', 'PEÇA', 42.00, 3.00, '');
INSERT INTO public.produtos (codigo, descricao, custounitario, precovenda, grupo, unidade, margemlucro, estoque, codean) VALUES (4, 'PISTOLA GLOCK G17 9MM DESERT BLACK', 8000.00, 10000.00, 'PISTOLAS', 'PEÇA', 50.00, 3.00, '');


--
-- Name: produtos_codigo; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.produtos_codigo', 7, true);


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.users (codigo, login, pass, adm) VALUES (1, 'adm', 'c4ca4238a0b923820dcc509a6f75849b', true);
INSERT INTO public.users (codigo, login, pass, adm) VALUES (2, 'thomas', 'c4ca4238a0b923820dcc509a6f75849b', true);
INSERT INTO public.users (codigo, login, pass, adm) VALUES (3, 'henrique', 'c4ca4238a0b923820dcc509a6f75849b', false);
INSERT INTO public.users (codigo, login, pass, adm) VALUES (5, 'teste', 'c4ca4238a0b923820dcc509a6f75849b', false);


--
-- Name: users_codigo; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_codigo', 5, true);


--
-- PostgreSQL database dump complete
--

