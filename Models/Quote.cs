using System;
public class Quote
{
    public long id { get; set; }
    public string company_name { get; set; }
    public string contact_name { get; set; }
    public string email { get; set; }
    public string product_line { get; set; }
    public string installation_fee { get; set; }
    public string sub_total { get; set; }
    public string total { get; set; }
    public string building_type { get; set; }
    public long num_of_floors { get; set; }
    public long num_of_apartments { get; set; }
    public long num_of_basements { get; set; }
    public long num_of_parking_spots { get; set; }
    public long num_of_distinct_businesses { get; set; }
    public long num_of_elevator_cages { get; set; }
    public long num_of_occupants_per_Floor { get; set; }
    public long num_of_activity_hours_per_day { get; set; }
    public long required_columns { get; set; }
    public long required_shafts { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }

}