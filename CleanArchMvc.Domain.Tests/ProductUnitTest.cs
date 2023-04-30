using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest
{
    private Product productFactory(int id, string name, string description, decimal price, int stock, string image)
    {
        return new Product(id, name, description, price, stock, image);
    }


    [Fact]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => productFactory(1, "Product Name", "Product Description", 9.99m,
            99, "product image");
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => productFactory(-1, "Product Name", "Product Description", 9.99m,
            99, "product image");

        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value.");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => productFactory(1, "Pr", "Product Description", 9.99m, 99,
            "product image");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
             .WithMessage("Invalid name. Too short, minimum 3 characters.");
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () => productFactory(1, "Product Name", "Product Description", 9.99m,
            99, "product image toooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooogggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");

        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
             .WithMessage("Invalid Image Name. Image name is too long (max 250 characters).");
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () => productFactory(1, "Product Name", "Product Description", 9.99m, 99, null);
        action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NullReferenceException()
    {
        Action action = () => productFactory(1, "Product Name", "Product Description", 9.99m, 99, null);
        action.Should().NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () => productFactory(1, "Product Name", "Product Description", 9.99m, 99, "");
        action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_InvalidPriceValue_DomainException()
    {
        Action action = () => productFactory(1, "Product Name", "Product Description", -9.99m,
            99, "");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
             .WithMessage("Invalid price value.");
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
    {
        Action action = () => productFactory(1, "Pro", "Product Description", 9.99m, value,
            "product image");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
               .WithMessage("Invalid stock value.");
    }

}

