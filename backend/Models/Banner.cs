public partial class Banner {
    public long Id { get; set; }
    public string Title { get; set; } =  null!;
    public string Slug  { get; set; } = null!;
    public string? Photo { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}