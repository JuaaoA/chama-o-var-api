# Diminuir a pontuação toda vez que uma ocorrência é criada
delimiter //

create trigger subtrair_pontuacao_torcedor
before insert on ocorrencias
for each row
begin

	update torcedores
    set score = score - new.penalidade
    where id = new.torcedor;

end //

delimiter ;