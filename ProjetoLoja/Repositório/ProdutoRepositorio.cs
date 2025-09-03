using MySql.Data.MySqlClient;
using ProjetoLoja.Models;

//ajuda a fazer um reconhecimneto do banco de dados
using Dapper;

namespace ProjetoLoja.Repositório
{
    public class ProdutoRepositorio
    {
        // criando uma string de conexão
        private readonly string _connectionString;
        
        // construtor que verifica a conexão
        public ProdutoRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Enumerable recebe uma lista(arrays)
        //Task é uma tarefa que é chamada com o async obrigatóriamente
        //Queryasync = sincronismo com o banco de dados em tempo real 
        public async Task<IEnumerable<Produto>> TodosProdutos()
        {
            using var connection = new MySqlConnection(_connectionString);
            var sql = "SELECT Id, Nome, Descricao, Preco, ImageUrl, Estoque FROM produto";
            return await connection.QueryAsync<Produto>(sql);
        }
    }
}
