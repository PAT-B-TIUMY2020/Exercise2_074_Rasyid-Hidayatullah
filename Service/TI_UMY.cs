﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-TT8EUJVL;Initial Catalog=TI UMY;Persist Security Info=True;User ID=sa; Password=123");

        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";
            string query = String.Format("insert into dbo.Mahasiswa values ('{0}','{1}','{2}','{3}')", mhs.nim, mhs.nama, mhs.prodi, mhs.angkatan);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                Console.WriteLine(query);
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "Sukses";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }


            return msg;
        }

        public string DeleteMahasiswa(string nim)
        {
            string query = string.Format("DELETE from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand cmd = new SqlCommand(query, con);
            int result = 0;
            string a = "Gagal";

            try
            {
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                a = ex.ToString();
            }

            if (result != 0)
            {
                a = "Sukses";
            }
            return a;
        }

        public List<Mahasiswa> GetAllMahasiswa()
        {
            {
                List<Mahasiswa> mahas = new List<Mahasiswa>();
                string query = "select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa";
                SqlCommand com = new SqlCommand(query, con); //yang dikirim ke sql

                try
                {
                    con.Open();
                    SqlDataReader reader = com.ExecuteReader(); //mendapatkan data yang telah dieksekusi, dari select, hasil query ditaro direader

                    while (reader.Read())
                    {
                        Mahasiswa mhs = new Mahasiswa();
                        mhs.nama = reader.GetString(0); //0 itu array pertama diambil dari iservice
                        mhs.nim = reader.GetString(1);
                        mhs.prodi = reader.GetString(2);
                        mhs.angkatan = reader.GetString(3);

                        mahas.Add(mhs);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(query);
                }
                return mahas;
            }
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            string query = String.Format("select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand com = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    mhs.nama = reader.GetString(0); //0 array pertama diambil dari iservice
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mhs;
        }

        public string UpdateMahasiswaByNIM(Mahasiswa mhs)
        {
            string msg = "Gagal";

            string query = string.Format("Update dbo.Mahasiswa set Nama = '{0}', Prodi = '{1}', Angkatan = '{2}' where NIM = '{3}'", mhs.nama, mhs.prodi, mhs.angkatan, mhs.nim);
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "Sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return msg;
        }
    }
}