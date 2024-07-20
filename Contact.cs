class Contact
{
    private string name;
    private string phone;

    public Contact()
    {
        name = "";
        phone = "";
    }

    public Contact(string nombre, string telefono)
    {
        name = nombre;
        phone = telefono;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Contact c = (Contact)obj;
        return Name == c.Name && Phone == c.Phone;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Phone);
    }
}