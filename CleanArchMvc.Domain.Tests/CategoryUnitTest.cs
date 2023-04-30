using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest
    {

        private Category categoryFactory(int id, string name)
        {
            return new Category(id, name);
        }

        [Fact(DisplayName = "Create Category with Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => categoryFactory(1, "Category Name");
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Exception if Create Category with Invalid Id ")]
        public void CreateCategory_WithInvalidId_DomainExceptionInvalid()
        {
            Action action = () => categoryFactory(-1, "Category Name");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid Id value.");
        }

        [Fact(DisplayName = "Exception if Create Category with Short Name")]
        public void CreateCategory_WithShortNameValue_DomainExceptionShortName()
        {
            Action action = () => categoryFactory(1, "Ca");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid name. Too short, minimum 3 characters.");
        }

        [Fact(DisplayName ="Exception if Name receives an Empty String")]
        public void CreateCategory_WithEmptyStringName_DomainExceptionInvalid()
        {
            Action action = () => categoryFactory(1, "");
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid name. Name is required.");
        }

        [Fact(DisplayName = "Exception if Name receives a null value")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalid()
        {
            Action action = () => categoryFactory(1, null);
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
          
        }
    }
}