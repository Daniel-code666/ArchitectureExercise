namespace Architecture.Application.Materials.Dtos
{
    public class MaterialDto
    {
        public string MaterialCode { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public decimal MaterialCost { get; set; }
    }

    public class MaterialReadDto : MaterialDto
    {
        public int MaterialId { get; set; }
    }
}
