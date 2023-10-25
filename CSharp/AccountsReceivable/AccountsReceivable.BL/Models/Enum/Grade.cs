namespace AccountsReceivable.BL.Models.Enum;

public enum GradeId : byte
{
    Bobby_BV = 0,
    Bobby_COND = 1,
    Bobby_DEAD = 2,
    Bull_M1 = 3,
    Bull_M2 = 4,
    Bull_M3 = 5,
    Bull_TM1 = 6,
    Bull_TM2 = 7,
    Bull_TM3 = 8,
    Bull_COND = 9,
    Bull_DEAD = 10,
    Cow_F1 = 11,
    Cow_F2 = 12,
    Cow_F3 = 13,
    Cow_P1 = 14,
    Cow_P2 = 15,
    Cow_P3 = 16,
    Cow_T1 = 17,
    Cow_T2 = 18,
    Cow_T3 = 19,
    Cow_COND = 20,
    Cow_DEAD = 21,
    MCow_M = 22,
    MCow_COND = 23,
    MCow_DEAD = 24,
    Heifer_A1 = 25,
    Heifer_A2 = 26,
    Heifer_A3 = 27,
    Heifer_F1 = 28,
    Heifer_F2 = 29,
    Heifer_F3 = 30,
    Heifer_L1 = 31,
    Heifer_L2 = 32,
    Heifer_L3 = 33,
    Heifer_M = 34,
    Heifer_P1 = 35,
    Heifer_P2 = 36,
    Heifer_P3 = 37,
    Heifer_T1 = 38,
    Heifer_T2 = 39,
    Heifer_T3 = 40,
    Heifer_COND = 41,
    Heifer_DEAD = 42,
    Steer_A1 = 43,
    Steer_A2 = 44,
    Steer_A3 = 45,
    Steer_F1 = 46,
    Steer_F2 = 47,
    Steer_F3 = 48,
    Steer_L1 = 49,
    Steer_L2 = 50,
    Steer_L3 = 51,
    Steer_M = 52,
    Steer_P1 = 53,
    Steer_P2 = 54,
    Steer_P3 = 55,
    Steer_T1 = 56,
    Steer_T2 = 57,
    Steer_T3 = 58,
    Steer_COND = 59,
    Steer_DEAD = 60,
    Lamb_A = 61,
    Lamb_B = 62,
    Lamb_C = 63,
    Lamb_F = 64,
    Lamb_M = 65,
    Lamb_P = 66,
    Lamb_T = 67,
    Lamb_Y = 68,
    Lamb_COND = 69,
    Lamb_DEAD = 70,
    Mutton_MF = 71,
    Mutton_MH = 72,
    Mutton_MM = 73,
    Mutton_MP = 74,
    Mutton_ML = 75,
    Mutton_MX = 76,
    Mutton_COND = 77,
    Mutton_DEAD = 78,
    Ram_R = 79,
    Ram_COND = 80,
    Ram_DEAD = 81,
    Hind_AF1 = 82,
    Hind_AF2 = 83,
    Hind_AFH = 84,
    Hind_AP = 85,
    Hind_M1 = 86,
    Hind_M2 = 87,
    Hind_PD1 = 88,
    Hind_PD2 = 89,
    Hind_PLG = 90,
    Hind_PLG1 = 91,
    Hind_PLG2 = 92,
    Hind_COND = 93,
    Hind_DEAD = 94,
    Stag_AF1 = 95,
    Stag_AF2 = 96,
    Stag_AFH = 97,
    Stag_AP = 98,
    Stag_M1 = 99,
    Stag_M2 = 100,
    Stag_PF1 = 101,
    Stag_PF2 = 102,
    Stag_PLG = 103,
    Stag_PLG1 = 104,
    Stag_PLG2 = 105,
    Stag_COND = 106,
    Stag_DEAD = 107
}

public class Grade
{
    /* Properties */

    public GradeId Id { get; set; }
    public AnimalTypeId AnimalTypeId { get; set; }
    public string Name { get; set; }

    /* Navigation */

    public virtual AnimalType AnimalType { get; set; }
}

public class GradeHelper
{
    private static readonly Dictionary<GradeId, Grade> _dictionary = new()
    {
        {
            GradeId.Bobby_BV,
            new Grade
            {
                Id = GradeId.Bobby_BV,
                AnimalTypeId = AnimalTypeId.Bobby,
                Name = "BV"
            }
        },
        {
            GradeId.Bobby_COND,
            new Grade
            {
                Id = GradeId.Bobby_COND,
                AnimalTypeId = AnimalTypeId.Bobby,
                Name = "COND"
            }
        },
        {
            GradeId.Bobby_DEAD,
            new Grade
            {
                Id = GradeId.Bobby_DEAD,
                AnimalTypeId = AnimalTypeId.Bobby,
                Name = "DEAD"
            }
        },

        {
            GradeId.Bull_M1,
            new Grade
            {
                Id = GradeId.Bull_M1,
                AnimalTypeId = AnimalTypeId.Bull,
                Name = "M1"
            }
        },
        {
            GradeId.Bull_M2,
            new Grade
            {
                Id = GradeId.Bull_M2,
                AnimalTypeId = AnimalTypeId.Bull,
                Name = "M2"
            }
        },
        {
            GradeId.Bull_M3,
            new Grade
            {
                Id = GradeId.Bull_M3,
                AnimalTypeId = AnimalTypeId.Bull,
                Name = "M3"
            }
        },
        {
            GradeId.Bull_TM1,
            new Grade
            {
                Id = GradeId.Bull_TM1,
                AnimalTypeId = AnimalTypeId.Bull,
                Name = "TM1"
            }
        },
        {
            GradeId.Bull_TM2,
            new Grade
            {
                Id = GradeId.Bull_TM2,
                AnimalTypeId = AnimalTypeId.Bull,
                Name = "TM2"
            }
        },
        {
            GradeId.Bull_TM3,
            new Grade
            {
                Id = GradeId.Bull_TM3,
                AnimalTypeId = AnimalTypeId.Bull,
                Name = "TM3"
            }
        },
        {
            GradeId.Bull_COND,
            new Grade
            {
                Id = GradeId.Bull_COND,
                AnimalTypeId = AnimalTypeId.Bull,
                Name = "COND"
            }
        },
        {
            GradeId.Bull_DEAD,
            new Grade
            {
                Id = GradeId.Bull_DEAD,
                AnimalTypeId = AnimalTypeId.Bull,
                Name = "DEAD"
            }
        },

        {
            GradeId.Cow_F1,
            new Grade
            {
                Id = GradeId.Cow_F1,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "F1"
            }
        },
        {
            GradeId.Cow_F2,
            new Grade
            {
                Id = GradeId.Cow_F2,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "F2"
            }
        },
        {
            GradeId.Cow_F3,
            new Grade
            {
                Id = GradeId.Cow_F3,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "F3"
            }
        },
        {
            GradeId.Cow_P1,
            new Grade
            {
                Id = GradeId.Cow_P1,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "P1"
            }
        },
        {
            GradeId.Cow_P2,
            new Grade
            {
                Id = GradeId.Cow_P2,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "P2"
            }
        },
        {
            GradeId.Cow_P3,
            new Grade
            {
                Id = GradeId.Cow_P3,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "P3"
            }
        },
        {
            GradeId.Cow_T1,
            new Grade
            {
                Id = GradeId.Cow_T1,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "T1"
            }
        },
        {
            GradeId.Cow_T2,
            new Grade
            {
                Id = GradeId.Cow_T2,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "T2"
            }
        },
        {
            GradeId.Cow_T3,
            new Grade
            {
                Id = GradeId.Cow_T3,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "T3"
            }
        },
        {
            GradeId.Cow_COND,
            new Grade
            {
                Id = GradeId.Cow_COND,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "COND"
            }
        },
        {
            GradeId.Cow_DEAD,
            new Grade
            {
                Id = GradeId.Cow_DEAD,
                AnimalTypeId = AnimalTypeId.Cow,
                Name = "DEAD"
            }
        },

        {
            GradeId.MCow_M,
            new Grade
            {
                Id = GradeId.MCow_M,
                AnimalTypeId = AnimalTypeId.MCow,
                Name = "M"
            }
        },
        {
            GradeId.MCow_COND,
            new Grade
            {
                Id = GradeId.MCow_COND,
                AnimalTypeId = AnimalTypeId.MCow,
                Name = "COND"
            }
        },
        {
            GradeId.MCow_DEAD,
            new Grade
            {
                Id = GradeId.MCow_DEAD,
                AnimalTypeId = AnimalTypeId.MCow,
                Name = "DEAD"
            }
        },

        {
            GradeId.Heifer_A1,
            new Grade
            {
                Id = GradeId.Heifer_A1,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "A1"
            }
        },
        {
            GradeId.Heifer_A2,
            new Grade
            {
                Id = GradeId.Heifer_A2,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "A2"
            }
        },
        {
            GradeId.Heifer_A3,
            new Grade
            {
                Id = GradeId.Heifer_A3,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "A3"
            }
        },
        {
            GradeId.Heifer_F1,
            new Grade
            {
                Id = GradeId.Heifer_F1,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "F1"
            }
        },
        {
            GradeId.Heifer_F2,
            new Grade
            {
                Id = GradeId.Heifer_F2,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "F2"
            }
        },
        {
            GradeId.Heifer_F3,
            new Grade
            {
                Id = GradeId.Heifer_F3,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "F3"
            }
        },
        {
            GradeId.Heifer_L1,
            new Grade
            {
                Id = GradeId.Heifer_L1,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "L1"
            }
        },
        {
            GradeId.Heifer_L2,
            new Grade
            {
                Id = GradeId.Heifer_L2,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "L2"
            }
        },
        {
            GradeId.Heifer_L3,
            new Grade
            {
                Id = GradeId.Heifer_L3,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "L3"
            }
        },
        {
            GradeId.Heifer_M,
            new Grade
            {
                Id = GradeId.Heifer_M,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "M"
            }
        },
        {
            GradeId.Heifer_P1,
            new Grade
            {
                Id = GradeId.Heifer_P1,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "P1"
            }
        },
        {
            GradeId.Heifer_P2,
            new Grade
            {
                Id = GradeId.Heifer_P2,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "P2"
            }
        },
        {
            GradeId.Heifer_P3,
            new Grade
            {
                Id = GradeId.Heifer_P3,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "P3"
            }
        },
        {
            GradeId.Heifer_T1,
            new Grade
            {
                Id = GradeId.Heifer_T1,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "T1"
            }
        },
        {
            GradeId.Heifer_T2,
            new Grade
            {
                Id = GradeId.Heifer_T2,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "T2"
            }
        },
        {
            GradeId.Heifer_T3,
            new Grade
            {
                Id = GradeId.Heifer_T3,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "T3"
            }
        },
        {
            GradeId.Heifer_COND,
            new Grade
            {
                Id = GradeId.Heifer_COND,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "COND"
            }
        },
        {
            GradeId.Heifer_DEAD,
            new Grade
            {
                Id = GradeId.Heifer_DEAD,
                AnimalTypeId = AnimalTypeId.Heifer,
                Name = "DEAD"
            }
        },

        {
            GradeId.Steer_A1,
            new Grade
            {
                Id = GradeId.Steer_A1,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "A1"
            }
        },
        {
            GradeId.Steer_A2,
            new Grade
            {
                Id = GradeId.Steer_A2,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "A2"
            }
        },
        {
            GradeId.Steer_A3,
            new Grade
            {
                Id = GradeId.Steer_A3,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "A3"
            }
        },
        {
            GradeId.Steer_F1,
            new Grade
            {
                Id = GradeId.Steer_F1,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "F1"
            }
        },
        {
            GradeId.Steer_F2,
            new Grade
            {
                Id = GradeId.Steer_F2,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "F2"
            }
        },
        {
            GradeId.Steer_F3,
            new Grade
            {
                Id = GradeId.Steer_F3,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "F3"
            }
        },
        {
            GradeId.Steer_L1,
            new Grade
            {
                Id = GradeId.Steer_L1,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "L1"
            }
        },
        {
            GradeId.Steer_L2,
            new Grade
            {
                Id = GradeId.Steer_L2,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "L2"
            }
        },
        {
            GradeId.Steer_L3,
            new Grade
            {
                Id = GradeId.Steer_L3,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "L3"
            }
        },
        {
            GradeId.Steer_M,
            new Grade
            {
                Id = GradeId.Steer_M,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "M"
            }
        },
        {
            GradeId.Steer_P1,
            new Grade
            {
                Id = GradeId.Steer_P1,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "P1"
            }
        },
        {
            GradeId.Steer_P2,
            new Grade
            {
                Id = GradeId.Steer_P2,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "P2"
            }
        },
        {
            GradeId.Steer_P3,
            new Grade
            {
                Id = GradeId.Steer_P3,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "P3"
            }
        },
        {
            GradeId.Steer_T1,
            new Grade
            {
                Id = GradeId.Steer_T1,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "T1"
            }
        },
        {
            GradeId.Steer_T2,
            new Grade
            {
                Id = GradeId.Steer_T2,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "T2"
            }
        },
        {
            GradeId.Steer_T3,
            new Grade
            {
                Id = GradeId.Steer_T3,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "T3"
            }
        },
        {
            GradeId.Steer_COND,
            new Grade
            {
                Id = GradeId.Steer_COND,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "COND"
            }
        },
        {
            GradeId.Steer_DEAD,
            new Grade
            {
                Id = GradeId.Steer_DEAD,
                AnimalTypeId = AnimalTypeId.Steer,
                Name = "DEAD"
            }
        },

        {
            GradeId.Lamb_A,
            new Grade
            {
                Id = GradeId.Lamb_A,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "A"
            }
        },
        {
            GradeId.Lamb_B,
            new Grade
            {
                Id = GradeId.Lamb_B,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "B"
            }
        },
        {
            GradeId.Lamb_C,
            new Grade
            {
                Id = GradeId.Lamb_C,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "C"
            }
        },
        {
            GradeId.Lamb_F,
            new Grade
            {
                Id = GradeId.Lamb_F,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "F"
            }
        },
        {
            GradeId.Lamb_M,
            new Grade
            {
                Id = GradeId.Lamb_M,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "M"
            }
        },
        {
            GradeId.Lamb_P,
            new Grade
            {
                Id = GradeId.Lamb_P,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "P"
            }
        },
        {
            GradeId.Lamb_T,
            new Grade
            {
                Id = GradeId.Lamb_T,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "T"
            }
        },
        {
            GradeId.Lamb_Y,
            new Grade
            {
                Id = GradeId.Lamb_Y,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "Y"
            }
        },
        {
            GradeId.Lamb_COND,
            new Grade
            {
                Id = GradeId.Lamb_COND,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "COND"
            }
        },
        {
            GradeId.Lamb_DEAD,
            new Grade
            {
                Id = GradeId.Lamb_DEAD,
                AnimalTypeId = AnimalTypeId.Lamb,
                Name = "DEAD"
            }
        },

        {
            GradeId.Mutton_MF,
            new Grade
            {
                Id = GradeId.Mutton_MF,
                AnimalTypeId = AnimalTypeId.Mutton,
                Name = "MF"
            }
        },
        {
            GradeId.Mutton_MH,
            new Grade
            {
                Id = GradeId.Mutton_MH,
                AnimalTypeId = AnimalTypeId.Mutton,
                Name = "MH"
            }
        },
        {
            GradeId.Mutton_MM,
            new Grade
            {
                Id = GradeId.Mutton_MM,
                AnimalTypeId = AnimalTypeId.Mutton,
                Name = "MM"
            }
        },
        {
            GradeId.Mutton_MP,
            new Grade
            {
                Id = GradeId.Mutton_MP,
                AnimalTypeId = AnimalTypeId.Mutton,
                Name = "MP"
            }
        },
        {
            GradeId.Mutton_ML,
            new Grade
            {
                Id = GradeId.Mutton_ML,
                AnimalTypeId = AnimalTypeId.Mutton,
                Name = "ML"
            }
        },
        {
            GradeId.Mutton_MX,
            new Grade
            {
                Id = GradeId.Mutton_MX,
                AnimalTypeId = AnimalTypeId.Mutton,
                Name = "MX"
            }
        },
        {
            GradeId.Mutton_COND,
            new Grade
            {
                Id = GradeId.Mutton_COND,
                AnimalTypeId = AnimalTypeId.Mutton,
                Name = "COND"
            }
        },
        {
            GradeId.Mutton_DEAD,
            new Grade
            {
                Id = GradeId.Mutton_DEAD,
                AnimalTypeId = AnimalTypeId.Mutton,
                Name = "DEAD"
            }
        },

        {
            GradeId.Ram_R,
            new Grade
            {
                Id = GradeId.Ram_R,
                AnimalTypeId = AnimalTypeId.Ram,
                Name = "R"
            }
        },
        {
            GradeId.Ram_COND,
            new Grade
            {
                Id = GradeId.Ram_COND,
                AnimalTypeId = AnimalTypeId.Ram,
                Name = "COND"
            }
        },
        {
            GradeId.Ram_DEAD,
            new Grade
            {
                Id = GradeId.Ram_DEAD,
                AnimalTypeId = AnimalTypeId.Ram,
                Name = "DEAD"
            }
        },

        {
            GradeId.Hind_AF1,
            new Grade
            {
                Id = GradeId.Hind_AF1,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "AF1"
            }
        },
        {
            GradeId.Hind_AF2,
            new Grade
            {
                Id = GradeId.Hind_AF2,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "AF2"
            }
        },
        {
            GradeId.Hind_AFH,
            new Grade
            {
                Id = GradeId.Hind_AFH,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "AFH"
            }
        },
        {
            GradeId.Hind_AP,
            new Grade
            {
                Id = GradeId.Hind_AP,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "AP"
            }
        },
        {
            GradeId.Hind_M1,
            new Grade
            {
                Id = GradeId.Hind_M1,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "M1"
            }
        },
        {
            GradeId.Hind_M2,
            new Grade
            {
                Id = GradeId.Hind_M2,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "M2"
            }
        },
        {
            GradeId.Hind_PD1,
            new Grade
            {
                Id = GradeId.Hind_PD1,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "PD1"
            }
        },
        {
            GradeId.Hind_PD2,
            new Grade
            {
                Id = GradeId.Hind_PD2,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "PD2"
            }
        },
        {
            GradeId.Hind_PLG,
            new Grade
            {
                Id = GradeId.Hind_PLG,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "PLG"
            }
        },
        {
            GradeId.Hind_PLG1,
            new Grade
            {
                Id = GradeId.Hind_PLG1,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "PLG1"
            }
        },
        {
            GradeId.Hind_PLG2,
            new Grade
            {
                Id = GradeId.Hind_PLG2,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "PLG2"
            }
        },
        {
            GradeId.Hind_COND,
            new Grade
            {
                Id = GradeId.Hind_COND,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "COND"
            }
        },
        {
            GradeId.Hind_DEAD,
            new Grade
            {
                Id = GradeId.Hind_DEAD,
                AnimalTypeId = AnimalTypeId.Hind,
                Name = "DEAD"
            }
        },

        {
            GradeId.Stag_AF1,
            new Grade
            {
                Id = GradeId.Stag_AF1,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "AF1"
            }
        },
        {
            GradeId.Stag_AF2,
            new Grade
            {
                Id = GradeId.Stag_AF2,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "AF2"
            }
        },
        {
            GradeId.Stag_AFH,
            new Grade
            {
                Id = GradeId.Stag_AFH,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "AFH"
            }
        },
        {
            GradeId.Stag_AP,
            new Grade
            {
                Id = GradeId.Stag_AP,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "AP"
            }
        },
        {
            GradeId.Stag_M1,
            new Grade
            {
                Id = GradeId.Stag_M1,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "M1"
            }
        },
        {
            GradeId.Stag_M2,
            new Grade
            {
                Id = GradeId.Stag_M2,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "M2"
            }
        },
        {
            GradeId.Stag_PF1,
            new Grade
            {
                Id = GradeId.Stag_PF1,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "PF1"
            }
        },
        {
            GradeId.Stag_PF2,
            new Grade
            {
                Id = GradeId.Stag_PF2,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "PF2"
            }
        },
        {
            GradeId.Stag_PLG,
            new Grade
            {
                Id = GradeId.Stag_PLG,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "PLG"
            }
        },
        {
            GradeId.Stag_PLG1,
            new Grade
            {
                Id = GradeId.Stag_PLG1,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "PLG1"
            }
        },
        {
            GradeId.Stag_PLG2,
            new Grade
            {
                Id = GradeId.Stag_PLG2,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "PLG2"
            }
        },
        {
            GradeId.Stag_COND,
            new Grade
            {
                Id = GradeId.Stag_COND,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "COND"
            }
        },
        {
            GradeId.Stag_DEAD,
            new Grade
            {
                Id = GradeId.Stag_DEAD,
                AnimalTypeId = AnimalTypeId.Stag,
                Name = "DEAD"
            }
        }
    };

    public static Grade GetInfo(GradeId id)
    {
        return (_dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<Grade> GetAll()
    {
        return _dictionary.Values;
    }
}