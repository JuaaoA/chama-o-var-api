create database chamaovar;
use chamaovar;

create table torcedores(
	id int auto_increment primary key,
    nome_completo varchar(40) not null,
    cpf varchar(11) not null unique,
    email varchar(30) not null unique,
    telefone varchar(15) not null unique,
    score int,
    nascimento date not null,
    senha varchar(50) not null,
    tecnico bool
);

create table tokens(
	id int auto_increment primary key,
    nome varchar(20) not null unique,
    torcedor int,
    foreign key(torcedor) references torcedores(id)
);

create table ocorrencias(
	id int auto_increment primary key,
    acontecimento varchar(40) not null,
    data_ocorrencia datetime not null,
    penalidade int,
    torcedor int not null,
    colaborador int not null,
    foreign key(torcedor) references torcedores(id),
    foreign key(colaborador) references torcedores(id),
    check(penalidade > 0)
);

create table eventos(
 id int auto_increment primary key,
 nome varchar(20) not null,
 data_evento datetime not null,
 detalhes varchar(40),
 minimo_pontuacao int not null,
 criador int not null,
 foreign key(criador) references torcedores(id),
 check(minimo_pontuacao >= 0 and minimo_pontuacao <= 1000)
);

create table ingressos(
	id int auto_increment primary key,
    torcedor int not null,
    evento int not null,
    foreign key(torcedor) references torcedores(id),
    foreign key(evento) references eventos(id)
);

select * from ocorrencias;
select * from torcedores;
select * from tokens;
select * from eventos;
select * from ingressos;