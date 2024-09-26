using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core;

public partial class HouseHubContext : DbContext
{
    public HouseHubContext()
    {
    }

    public HouseHubContext(DbContextOptions<HouseHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agendamento> Agendamentos { get; set; }

    public virtual DbSet<Avaliacao> Avaliacaos { get; set; }

    public virtual DbSet<Imagem> Imagems { get; set; }

    public virtual DbSet<Imovel> Imovels { get; set; }

    public virtual DbSet<Locacao> Locacaos { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<Resultadoavaliacao> Resultadoavaliacaos { get; set; }

    public virtual DbSet<Solicitacaoreparo> Solicitacaoreparos { get; set; }

    public virtual DbSet<Valore> Valores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=m4rcos;database=househub");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agendamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("agendamento");

            entity.HasIndex(e => e.IdImovel, "fk_Agendamento_Imovel1_idx");

            entity.HasIndex(e => e.IdPessoa, "fk_Agendamento_Pessoa1_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("date")
                .HasColumnName("dataCriacao");
            entity.Property(e => e.DataVisita)
                .HasColumnType("date")
                .HasColumnName("dataVisita");
            entity.Property(e => e.IdImovel).HasColumnName("idImovel");
            entity.Property(e => e.IdPessoa).HasColumnName("idPessoa");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(50)
                .HasColumnName("observacoes");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Pendente'")
                .HasColumnType("enum('Pendente','Recusado','Agendado','Concluído')")
                .HasColumnName("status");

            entity.HasOne(d => d.IdImovelNavigation).WithMany(p => p.Agendamentos)
                .HasForeignKey(d => d.IdImovel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_Agendamento_Imovel1");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.Agendamentos)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_Agendamento_Pessoa1");
        });

        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("avaliacao");

            entity.HasIndex(e => e.IdPessoa, "fk_Avaliacao_Pessoa1_idx");

            entity.HasIndex(e => e.ResultadoAvaliacaoId, "fk_Avaliacao_ResultadoAvaliacao1_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Documento)
                .HasMaxLength(40)
                .HasColumnName("documento");
            entity.Property(e => e.IdPessoa).HasColumnName("idPessoa");
            entity.Property(e => e.NumeroDependentes).HasColumnName("numeroDependentes");
            entity.Property(e => e.RendaMensal)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("rendaMensal");
            entity.Property(e => e.ResultadoAvaliacaoId).HasColumnName("ResultadoAvaliacao_id");
            entity.Property(e => e.ScoreSerasa).HasColumnName("scoreSerasa");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Solicitado'")
                .HasColumnType("enum('Solicitado','Análise','Aprovado','Reprovado')")
                .HasColumnName("status");
            entity.Property(e => e.ValorAprovado)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("valorAprovado");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.Avaliacaos)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_Avaliacao_Pessoa1");

            entity.HasOne(d => d.ResultadoAvaliacao).WithMany(p => p.Avaliacaos)
                .HasForeignKey(d => d.ResultadoAvaliacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Avaliacao_ResultadoAvaliacao1");
        });

        modelBuilder.Entity<Imagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("imagem");

            entity.HasIndex(e => e.Url, "URL_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Imovel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("imovel");

            entity.HasIndex(e => e.Bairro, "bairro_idx");

            entity.HasIndex(e => e.Cidade, "cidade_idx");

            entity.HasIndex(e => e.Descricao, "descricao_idx");

            entity.HasIndex(e => e.Estado, "estado_idx");

            entity.HasIndex(e => e.IdPessoa, "fk_Imovel_Pessoa1_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Logradouro, "logradouro_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bairro)
                .HasMaxLength(100)
                .HasColumnName("bairro");
            entity.Property(e => e.Banheiros).HasColumnName("banheiros");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(100)
                .HasColumnName("cidade");
            entity.Property(e => e.Complemento)
                .HasMaxLength(100)
                .HasColumnName("complemento");
            entity.Property(e => e.Descricao)
                .HasMaxLength(40)
                .HasColumnName("descricao");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .HasColumnName("estado");
            entity.Property(e => e.IdPessoa).HasColumnName("idPessoa");
            entity.Property(e => e.Iptu)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("iptu");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(45)
                .HasColumnName("logradouro");
            entity.Property(e => e.Numero)
                .HasMaxLength(45)
                .HasColumnName("numero");
            entity.Property(e => e.PodeAnimal)
                .HasComment("0 = Não, 1 = Sim")
                .HasColumnName("podeAnimal");
            entity.Property(e => e.PrecoAluguel)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("precoAluguel");
            entity.Property(e => e.PrecoCondominio)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("precoCondominio");
            entity.Property(e => e.PrecoVenda)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("precoVenda");
            entity.Property(e => e.Quartos).HasColumnName("quartos");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Disponível'")
                .HasColumnType("enum('Disponível','Vendido','Alugado')")
                .HasColumnName("status");
            entity.Property(e => e.Tipo)
                .HasDefaultValueSql("'Casa'")
                .HasColumnType("enum('Casa','Apartamento')")
                .HasColumnName("tipo");
            entity.Property(e => e.ValorCondominio)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("valorCondominio");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.Imovels)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_Imovel_Pessoa1");

            entity.HasMany(d => d.Imagems).WithMany(p => p.Imovels)
                .UsingEntity<Dictionary<string, object>>(
                    "Imovelimagem",
                    r => r.HasOne<Imagem>().WithMany()
                        .HasForeignKey("ImagemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Imovel_has_Imagem_Imagem1"),
                    l => l.HasOne<Imovel>().WithMany()
                        .HasForeignKey("ImovelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Imovel_has_Imagem_Imovel1"),
                    j =>
                    {
                        j.HasKey("ImovelId", "ImagemId").HasName("PRIMARY");
                        j.ToTable("imovelimagem");
                        j.HasIndex(new[] { "ImagemId" }, "fk_Imovel_has_Imagem_Imagem1_idx");
                        j.HasIndex(new[] { "ImovelId" }, "fk_Imovel_has_Imagem_Imovel1_idx");
                        j.IndexerProperty<uint>("ImovelId").HasColumnName("Imovel_id");
                        j.IndexerProperty<uint>("ImagemId").HasColumnName("Imagem_id");
                    });
        });

        modelBuilder.Entity<Locacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("locacao");

            entity.HasIndex(e => e.IdImovel, "fk_Locacao_Imovel1_idx");

            entity.HasIndex(e => e.IdPessoa, "fk_Locacao_Pessoa1_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataContrato)
                .HasColumnType("date")
                .HasColumnName("dataContrato");
            entity.Property(e => e.DataFim)
                .HasColumnType("date")
                .HasColumnName("dataFim");
            entity.Property(e => e.DataInicio)
                .HasColumnType("date")
                .HasColumnName("dataInicio");
            entity.Property(e => e.DataVencimento)
                .HasColumnType("date")
                .HasColumnName("dataVencimento");
            entity.Property(e => e.IdImovel).HasColumnName("idImovel");
            entity.Property(e => e.IdPessoa).HasColumnName("idPessoa");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Ativo'")
                .HasColumnType("enum('Ativo','Inativo')")
                .HasColumnName("status");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdImovelNavigation).WithMany(p => p.Locacaos)
                .HasForeignKey(d => d.IdImovel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_Locacao_Imovel1");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.Locacaos)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_Locacao_Pessoa1");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pagamento");

            entity.HasIndex(e => e.IdLocacao, "fk_Pagamento_Locacao1_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataPagamento)
                .HasColumnType("date")
                .HasColumnName("dataPagamento");
            entity.Property(e => e.DataVencimento)
                .HasColumnType("date")
                .HasColumnName("dataVencimento");
            entity.Property(e => e.FormaPagamento)
                .HasColumnType("enum('Dinheiro','Cartão de Crédito','Transferência Bancária','Boleto','Pix')")
                .HasColumnName("formaPagamento");
            entity.Property(e => e.IdLocacao).HasColumnName("idLocacao");
            entity.Property(e => e.PagamentoManual)
                .HasComment("0 = Não, 1 = Sim")
                .HasColumnName("pagamentoManual");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Pendente'")
                .HasColumnType("enum('Pendente','Pago','Em atraso')")
                .HasColumnName("status");

            entity.HasOne(d => d.IdLocacaoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdLocacao)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_Pagamento_Locacao1");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pessoa");

            entity.HasIndex(e => e.Cpf, "cpf_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Telefone, "telefone_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bairro)
                .HasMaxLength(50)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(45)
                .HasColumnName("cidade");
            entity.Property(e => e.Complemento)
                .HasMaxLength(100)
                .HasColumnName("complemento");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("cpf");
            entity.Property(e => e.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("dataNascimento");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasMaxLength(45)
                .HasColumnName("estado");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(100)
                .HasColumnName("logradouro");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .HasColumnName("numero");
            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Resultadoavaliacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("resultadoavaliacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aprovado)
                .HasDefaultValueSql("'0'")
                .HasComment("0 - Não\n1 - Sim")
                .HasColumnName("aprovado");
            entity.Property(e => e.DataFinalizado)
                .HasColumnType("date")
                .HasColumnName("dataFinalizado");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<Solicitacaoreparo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("solicitacaoreparo");

            entity.HasIndex(e => e.Descricao, "descricao_idx");

            entity.HasIndex(e => e.IdLocacao, "fk_SolicitacaoReparo_Locacao1_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .HasColumnName("descricao");
            entity.Property(e => e.EnviarAlguem)
                .HasDefaultValueSql("'1'")
                .HasComment("0 = Não, 1 = Sim")
                .HasColumnName("enviarAlguem");
            entity.Property(e => e.IdLocacao).HasColumnName("idLocacao");
            entity.Property(e => e.RespostaProprietario)
                .HasMaxLength(100)
                .HasColumnName("respostaProprietario");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Aberto'")
                .HasColumnType("enum('Aberto','Avaliação','Concluído')")
                .HasColumnName("status");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10,0) unsigned")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdLocacaoNavigation).WithMany(p => p.Solicitacaoreparos)
                .HasForeignKey(d => d.IdLocacao)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_SolicitacaoReparo_Locacao1");

            entity.HasMany(d => d.Imagems).WithMany(p => p.SolicitacaoReparos)
                .UsingEntity<Dictionary<string, object>>(
                    "Solicitacaoreparoimagem",
                    r => r.HasOne<Imagem>().WithMany()
                        .HasForeignKey("ImagemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_SolicitacaoReparo_has_Imagem_Imagem1"),
                    l => l.HasOne<Solicitacaoreparo>().WithMany()
                        .HasForeignKey("SolicitacaoReparoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_SolicitacaoReparo_has_Imagem_SolicitacaoReparo1"),
                    j =>
                    {
                        j.HasKey("SolicitacaoReparoId", "ImagemId").HasName("PRIMARY");
                        j.ToTable("solicitacaoreparoimagem");
                        j.HasIndex(new[] { "ImagemId" }, "fk_SolicitacaoReparo_has_Imagem_Imagem1_idx");
                        j.HasIndex(new[] { "SolicitacaoReparoId" }, "fk_SolicitacaoReparo_has_Imagem_SolicitacaoReparo1_idx");
                        j.IndexerProperty<uint>("SolicitacaoReparoId").HasColumnName("SolicitacaoReparo_id");
                        j.IndexerProperty<uint>("ImagemId").HasColumnName("Imagem_id");
                    });
        });

        modelBuilder.Entity<Valore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("valores");

            entity.HasIndex(e => e.Descricao, "Descricao_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .HasMaxLength(45)
                .HasColumnName("descricao");
            entity.Property(e => e.Valor)
                .HasPrecision(10)
                .HasColumnName("valor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
