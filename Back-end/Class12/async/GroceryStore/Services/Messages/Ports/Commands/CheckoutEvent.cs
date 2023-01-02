﻿using NServiceBus;
using Paramore.Brighter;
using System;
using System.Collections.Generic;

namespace CasaDoCodigo.Mensagens.Ports.Commands
{
    public class CheckoutEvent : Event
    {
        public CheckoutEvent() : base(Guid.NewGuid()) { }

        public CheckoutEvent(string clienteId, List<CheckoutItem> items) : base(Guid.NewGuid())
        {
            ClienteId = clienteId;
            Items = items;
        }

        public string ClienteId { get; set; }
        public IList<CheckoutItem> Items { get; set; }
    }

    public class CheckoutItem
    {
        public CheckoutItem()
        {

        }

        public CheckoutItem(string id, string produtoId, string produtoNome, decimal precoUnitario, int quantidade, string urlImagem)
        {
            Id = id;
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
            UrlImagem = urlImagem;
        }

        public string Id { get; set; }
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public string UrlImagem { get; set; }
    }
}
