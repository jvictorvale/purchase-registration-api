﻿using CadastroDeCompras.Domain.Validation;

namespace CadastroDeCompras.Domain.Entities
{
    public sealed class Person
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Phone { get; private set; }
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<PersonImage> PersonImages { get; private set; }

        public Person(string name, string document, string phone)
        {
            Validation(name, document, phone);
            Purchases = new List<Purchase>();
            PersonImages = new List<PersonImage>();
        }

        public Person(int id, string name, string document, string phone)
        {
            DomainValidationException.When(id <= 0, "Id da pessoa deve ser informado!");
            Id = id;
            Validation(name, document, phone);
            Purchases = new List<Purchase>();
            PersonImages = new List<PersonImage>();
        }

        private void Validation(string name, string document, string phone)
        {
            DomainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(document),"Documento deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(phone), "Celular deve ser informado!");
            
            Name = name;
            Document = document;
            Phone = phone;
        }
    }
}