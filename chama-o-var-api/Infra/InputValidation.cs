using System;
namespace chama_o_var_api.Infra
{
	public static class InputValidation
	{
		// Verificação do email
		public static bool ValidarEmail(string email)
		{
			// Tirar espaços nos cantos
			email = email.Trim();

			// Se termina em .
			if (email.EndsWith("."))
			{
				return false;
			}

			try
			{
				// Verifica se o endereço faz sentido como um email
				var enderecar = new System.Net.Mail.MailAddress(email);
				return enderecar.Address == email;
			}
			catch
			{
				// Se não for, voltar como false
				return false;
			}
		}

		// Validação de CPF
		public static bool ValidarCPF(string cpf)
		{
			// Caso o cpf seja menor que 11 e não seja numérico
			return !(cpf.Length != 11);
		}

		// Validação da data nascimento
		public static bool ValidarDataNasc(DateTime nascimento)
		{
			// Calcular idade
			int idade = DateTime.Now.Year - nascimento.Year;

			// Retornar se a idade é maior que 14
			return idade >= 14;
		}

		// Validação de telefone
		public static bool ValidarTelefone(string telefone)
		{
			/*
			 * MODELO TELEFONE 27990340396
			 */
			return !(telefone.Length != 11);
		}
	}
}

