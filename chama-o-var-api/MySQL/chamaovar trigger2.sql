# Diminuir a pontuação toda vez que uma ocorrência é criada
delimiter //

create trigger adicionar_pontuacao_torcedor
before delete on ocorrencias
for each row
begin

	update torcedores
    set score = score + old.penalidade
    where id = old.torcedor;

end //

delimiter ; 
