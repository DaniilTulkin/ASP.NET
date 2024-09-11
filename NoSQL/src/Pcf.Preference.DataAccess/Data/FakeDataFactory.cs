namespace Pcf.Preference.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static List<Core.Domain.Preference> Preferences =>
        [
            new Core.Domain.Preference()
            {
                Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new Core.Domain.Preference()
            {
                Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new Core.Domain.Preference()
            {
                Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        ];


    }
}
