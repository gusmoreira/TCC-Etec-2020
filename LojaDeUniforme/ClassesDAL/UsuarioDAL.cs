﻿using LojaDeUniforme.ClassesBLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LojaDeUniforme.ClassesDAL
{
    class UsuarioDAL
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        #region Select All
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dataTable = new DataTable();
            try
            {
                String sql = "SELECT * FROM tb_Usuarios";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }
        #endregion
        #region Insert
        public bool Insert(UsuarioBLL bLL)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                String sql = "INSERT INTO tb_Usuarios(nome, nomedeusuario, senha,  dataultimamodificacao, usuarioultimamodificacao) VALUES (@nome, @nomedeusuario, @senha, @dataultimamodificacao, @usuarioultimamodificacao)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", bLL.nome);
                cmd.Parameters.AddWithValue("@nomedeusuario", bLL.nomedeusuario);
                cmd.Parameters.AddWithValue("@senha", bLL.senha);
                cmd.Parameters.AddWithValue("@dataultimamodificacao", bLL.dataultimamodificacao);
                cmd.Parameters.AddWithValue("@usuarioultimamodificacao", bLL.usuarioultimamodificacao);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();

            }
            return isSuccess;
        }
        #endregion
        #region Update
        public bool Update(UsuarioBLL bLL)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "UPDATE tb_Usuarios SET nome=@nome, nomedeusuario=@nomedeusuario, senha=@senha,  dataultimamodificacao=@dataultimamodificacao, usuarioultimamodificacao=@usuarioultimamodificacao WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@id", bLL.id);
                cmd.Parameters.AddWithValue("@nome", bLL.nome);
                cmd.Parameters.AddWithValue("@nomedeusuario", bLL.nomedeusuario);
                cmd.Parameters.AddWithValue("@senha", bLL.senha);
                cmd.Parameters.AddWithValue("@dataultimamodificacao", bLL.dataultimamodificacao);
                cmd.Parameters.AddWithValue("@usuarioultimamodificacao", bLL.usuarioultimamodificacao);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Delete
        public bool Delete (UsuarioBLL bLL)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "DELETE FROM tb_Usuarios WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", bLL.id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Search
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tb_Usuarios WHERE id LIKE '%"+keywords+"%' OR nome LIKE '%"+keywords+"%' OR nomedeusuario LIKE '%"+keywords+"%' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
        #region Selecionar Atraves Do Nome Do Usuario
        public UsuarioBLL SelecionarAtravesDoNomeDoUsuario (string nomedeusuario)
        {
            UsuarioBLL bll = new UsuarioBLL();
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT nomedeusuario FROM tb_Usuarios WHERE nomedeusuario = '" + nomedeusuario + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    bll.usuarioultimamodificacao = dt.Rows[0]["nomedeusuario"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return bll;
        }
        #endregion
    }
}