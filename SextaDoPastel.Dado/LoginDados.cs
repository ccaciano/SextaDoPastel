using Dapper;
using SextaDoPastel.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SextaDoPastel.Dado
{
    public class LoginDados
    {
        /// <summary>
        /// PESQUISA USUÁRIOS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Login> Selecionar()
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                var lista = connection.Query<Login>($"SELECT * " +
                                                  $"FROM [TB_LOGIN] ");
                return lista;
            }
        }

        /// <summary>
        /// PESQUISA USUÁRIO POR ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Login SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Login>($"SELECT * " +
                                                                $"FROM [TB_LOGIN] " +
                                                                $"WHERE ID_LOG = {id}");
                return obj;
            }
        }

        /// <summary>
        /// PESQUISA USUÁRIO POR EMAIL
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Login SelecionarPorEmail(string email)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Login>($"SELECT * " +
                                                                $"FROM [TB_LOGIN] " +
                                                                $"WHERE EMAIL = '{email}'");
                return obj;
            }
        }

        /// <summary>
        /// PESQUISA USUÁRIO POR NICK
        /// </summary>
        /// <param name="nick"></param>
        /// <returns></returns>
        public Login SelecionarPorNick(string nick)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Login>($"SELECT * " +
                                                                $"FROM [TB_LOGIN] " +
                                                                $"WHERE NICK = '{nick}'");
                return obj;
            }
        }

        /// <summary>
        /// REALIZA LOGIN PELO EMAIL
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public Login EfetuarLoginEmail(string email, string senha)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Login>($"DECLARE @HASH VARCHAR(32); " +
                                                                $"SET @HASH = '{senha}' " +
                                                                $"SET @HASH = CONVERT(VARCHAR(32), HashBytes('MD5', @HASH), 2)" +
                                                                $"SET @HASH = CONVERT(VARCHAR(32), HashBytes('MD5', @HASH), 2)" +
                                                                $"SELECT * " +
                                                                $"FROM [TB_LOGIN] " +
                                                                $"WHERE EMAIL = '{email}' AND SENHA = @HASH");
                return obj;
            }
        }

        /// <summary>
        /// REALIZA LOGIN PELO NICK
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public Login EfetuarLoginNick(string nick, string senha)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Login>($"DECLARE @HASH VARCHAR(32); " +
                                                                $"SET @HASH = '{senha}' " +
                                                                $"SET @HASH = CONVERT(VARCHAR(32), HashBytes('MD5', @HASH), 2)" +
                                                                $"SET @HASH = CONVERT(VARCHAR(32), HashBytes('MD5', @HASH), 2)" +
                                                                $"SELECT * " +
                                                                $"FROM [TB_LOGIN] " +
                                                                $"WHERE NICK = '{nick}' AND SENHA = @HASH");
                return obj;
            }
        }

        /// <summary>
        /// CADASTRA LOGIN DE USUARIO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Login entity)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID INT; " +
                                                   $"DECLARE @HASH VARCHAR(32); " +
                                                   $"SET @HASH = '{entity.SENHA}' " +
                                                   $"SET @HASH = CONVERT(VARCHAR(32), HashBytes('MD5', @HASH), 2)" +
                                                   $"SET @HASH = CONVERT(VARCHAR(32), HashBytes('MD5', @HASH), 2)" +
                                                   $"INSERT INTO [TB_LOGIN] " +
                                                   $"(EMAIL, NICK, SENHA, PERFIL, STATUS) " +
                                                   $"VALUES ('{entity.EMAIL}', " +
                                                   $"'{entity.NICK}', " +
                                                   $"@HASH, 0, 1)" +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// ALTERA SENHA DO USUÁRIO
        /// </summary>
        /// <param name="entity"></param>
        public void AlterarSenha(Login entity)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                connection.Execute($"DECLARE @HASH VARCHAR(32); " +
                                   $"SET @HASH = '{entity.SENHA}' " +
                                   $"SET @HASH = CONVERT(VARCHAR(32), HashBytes('MD5', @HASH), 2)" +
                                   $"SET @HASH = CONVERT(VARCHAR(32), HashBytes('MD5', @HASH), 2)" +
                                   $"UPDATE [TB_LOGIN] " +
                                   $"SET SENHA = @HASH " +
                                   $"WHERE ID_LOG = {entity.ID_LOG}");
            }
        }

        /// <summary>
        /// ALTERA EMAIL E NICK DO USUÁRIO
        /// </summary>
        /// <param name="entity"></param>
        public void AlterarEmailNick(Login entity)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                connection.Execute($"UPDATE [TB_LOGIN] " +
                                   $"SET EMAIL = '{entity.EMAIL}', " +
                                   $"NICK = '{entity.NICK}' " +
                                   $"WHERE ID_LOG = {entity.ID_LOG}");
            }
        }

        /// <summary>
        /// ALTERA STATUS "ATIVO" / "INATIVO"
        /// </summary>
        /// <param name="entity"></param>
        public void AlterarStatus(Login entity)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                connection.Execute($"UPDATE [TB_LOGIN] " +
                                   $"SET STATUS = {entity.STATUS} " +
                                   $"WHERE ID_LOG = {entity.ID_LOG}");
            }
        }

        /// <summary>
        /// ALTERA PERFIL "USER" / "ADM"
        /// </summary>
        /// <param name="entity"></param>
        public void AlterarPerfil(Login entity)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                connection.Execute($"UPDATE [TB_LOGIN] " +
                                   $"SET PERFIL = {entity.PERFIL} " +
                                   $"WHERE ID_LOG = {entity.ID_LOG}");
            }
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public void Deletar(int id)
        {
            using (var connection = new SqlConnection(dbConnection.ConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [TB_LOGIN] " +
                                   $"WHERE ID_LOG = {id}");
            }
        }
    }
}
