<Query Kind="Statements">
  <NuGetReference>Microsoft.Owin</NuGetReference>
  <NuGetReference>Microsoft.Owin.Security</NuGetReference>
  <NuGetReference>State.OR.Oya.Core.Utility</NuGetReference>
  <Namespace>Microsoft.Owin</Namespace>
  <Namespace>Microsoft.Owin.Security.DataHandler.Encoder</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Caching</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Diagnostics</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.IO</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Logging</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Reflection</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.ServiceClient</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.String</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Threading</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Xml</Namespace>
</Query>

var a = System.Text.Encoding.Default.GetString(Microsoft.Owin.Security.DataHandler.Encoder.TextEncodings.Base64Url.Decode("ZDBlNGI1NGYyODg3NGM3OTRiZmVjMjAzNDZmNzMxMDk"));
a.Dump();
a.Length.Dump();
/*
var s = @"[
  {
    ""_id"": ""56fd4829b749edbfb94f964a"",
    ""index"": 0,
    ""guid"": ""c7143a53-795b-4495-a66c-6a9a59f79807"",
    ""isActive"": false,
    ""balance"": ""$2,673.88"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 31,
    ""eyeColor"": ""blue"",
    ""name"": ""Sears Brock"",
    ""gender"": ""male"",
    ""company"": ""DARWINIUM"",
    ""email"": ""searsbrock@darwinium.com"",
    ""phone"": ""+1 (876) 427-3044"",
    ""address"": ""558 Jackson Street, Bison, Idaho, 7974"",
    ""about"": ""Est irure mollit velit ad aliqua consectetur reprehenderit. Excepteur laboris ullamco voluptate exercitation. Cillum anim minim culpa proident irure ea qui enim elit occaecat aliqua dolor. Eu sit nostrud nisi fugiat nisi pariatur ut aliquip dolor ut enim commodo. Incididunt sit adipisicing incididunt est sint aute.\r\n"",
    ""registered"": ""2015-11-04T07:12:12 +08:00"",
    ""latitude"": 21.713578,
    ""longitude"": -8.70294,
    ""tags"": [
      ""ut"",
      ""dolor"",
      ""anim"",
      ""voluptate"",
      ""dolore"",
      ""eu"",
      ""et""
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Nell Mejia""
      },
      {
        ""id"": 1,
        ""name"": ""Dina Brennan""
      },
      {
        ""id"": 2,
        ""name"": ""Oneal Lawrence""
      }
    ],
    ""greeting"": ""Hello, Sears Brock! You have 4 unread messages."",
    ""favoriteFruit"": ""banana""
  },
  {
    ""_id"": ""56fd48296c70aeee5cf4f5a7"",
    ""index"": 1,
    ""guid"": ""23a2ce87-d85d-4e0d-a1b6-53547f6c12c7"",
    ""isActive"": false,
    ""balance"": ""$1,880.75"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 38,
    ""eyeColor"": ""green"",
    ""name"": ""Adeline Livingston"",
    ""gender"": ""female"",
    ""company"": ""NEPTIDE"",
    ""email"": ""adelinelivingston@neptide.com"",
    ""phone"": ""+1 (821) 544-3077"",
    ""address"": ""547 Veterans Avenue, Topanga, Kansas, 865"",
    ""about"": ""Tempor ea elit cillum ea aliquip nostrud eu. Id est velit occaecat veniam enim magna in ad culpa consequat labore. Deserunt aute esse qui nostrud qui dolore aliqua proident incididunt et occaecat et duis labore. Eiusmod aliquip est aute excepteur eu quis eu reprehenderit eiusmod non fugiat consectetur pariatur. Aliquip aute adipisicing excepteur commodo qui ea pariatur qui sint et. Qui eiusmod magna ea deserunt dolore enim culpa elit. Duis ad dolor quis duis do duis laborum nulla aute fugiat esse officia tempor.\r\n"",
    ""registered"": ""2014-09-28T06:11:21 +07:00"",
    ""latitude"": -37.617884,
    ""longitude"": 56.204109,
    ""tags"": [
      ""cupidatat"",
      ""deserunt"",
      ""fugiat"",
      ""culpa"",
      ""anim"",
      ""laborum"",
      ""enim""
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Abbott Acevedo""
      },
      {
        ""id"": 1,
        ""name"": ""Pat Herrera""
      },
      {
        ""id"": 2,
        ""name"": ""Dean Holt""
      }
    ],
    ""greeting"": ""Hello, Adeline Livingston! You have 7 unread messages."",
    ""favoriteFruit"": ""apple""
  },
  {
    ""_id"": ""56fd482954b527b5a0b1bb59"",
    ""index"": 2,
    ""guid"": ""cb820869-abd4-4779-9ba0-7d4d72d33d6f"",
    ""isActive"": true,
    ""balance"": ""$2,987.95"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 33,
    ""eyeColor"": ""green"",
    ""name"": ""Raquel York"",
    ""gender"": ""female"",
    ""company"": ""TEMORAK"",
    ""email"": ""raquelyork@temorak.com"",
    ""phone"": ""+1 (945) 424-2824"",
    ""address"": ""671 Turner Place, Boykin, Colorado, 1128"",
    ""about"": ""Exercitation ea anim cillum eiusmod veniam non cillum qui sit anim. Cupidatat dolor ut sit aliqua labore laborum cupidatat dolore irure eiusmod commodo non. Nostrud proident amet ad nostrud pariatur fugiat quis consectetur duis est mollit dolore culpa.\r\n"",
    ""registered"": ""2014-10-06T02:28:33 +07:00"",
    ""latitude"": -72.512287,
    ""longitude"": -96.485911,
    ""tags"": [
      ""non"",
      ""sunt"",
      ""duis"",
      ""adipisicing"",
      ""enim"",
      ""velit"",
      ""id""
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Hammond Burt""
      },
      {
        ""id"": 1,
        ""name"": ""Johnston Irwin""
      },
      {
        ""id"": 2,
        ""name"": ""Celina Lambert""
      }
    ],
    ""greeting"": ""Hello, Raquel York! You have 3 unread messages."",
    ""favoriteFruit"": ""apple""
  },
  {
    ""_id"": ""56fd4829aa3ee27576cd1a90"",
    ""index"": 3,
    ""guid"": ""e3506bb8-4d47-4edb-8461-af66e3918c0e"",
    ""isActive"": false,
    ""balance"": ""$1,403.20"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 40,
    ""eyeColor"": ""green"",
    ""name"": ""Mayo Mckinney"",
    ""gender"": ""male"",
    ""company"": ""ACCRUEX"",
    ""email"": ""mayomckinney@accruex.com"",
    ""phone"": ""+1 (851) 502-2403"",
    ""address"": ""159 Boardwalk , Helen, Alabama, 5364"",
    ""about"": ""Eiusmod non labore eiusmod ex eiusmod id sint cillum. Ea quis sunt excepteur tempor sit. Est in culpa laborum ullamco in sunt sit excepteur deserunt ut consequat consequat fugiat. Do et minim magna fugiat Lorem consectetur. Dolore ut est laborum sit minim magna aliquip nostrud cillum. Est elit tempor consequat elit nisi amet.\r\n"",
    ""registered"": ""2014-05-21T12:39:58 +07:00"",
    ""latitude"": -36.777078,
    ""longitude"": -103.929668,
    ""tags"": [
      ""sunt"",
      ""est"",
      ""esse"",
      ""aliqua"",
      ""Lorem"",
      ""laborum"",
      ""magna""
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Martha Castaneda""
      },
      {
        ""id"": 1,
        ""name"": ""Roslyn Bennett""
      },
      {
        ""id"": 2,
        ""name"": ""Lowe Benjamin""
      }
    ],
    ""greeting"": ""Hello, Mayo Mckinney! You have 7 unread messages."",
    ""favoriteFruit"": ""strawberry""
  },
  {
    ""_id"": ""56fd4829f59c6b795a620540"",
    ""index"": 4,
    ""guid"": ""dd14031c-f3c3-4caf-ab67-2e086f403fd9"",
    ""isActive"": true,
    ""balance"": ""$1,058.09"",
    ""picture"": ""http://placehold.it/32x32"",
    ""age"": 32,
    ""eyeColor"": ""blue"",
    ""name"": ""Mindy Velasquez"",
    ""gender"": ""female"",
    ""company"": ""COLLAIRE"",
    ""email"": ""mindyvelasquez@collaire.com"",
    ""phone"": ""+1 (998) 403-3462"",
    ""address"": ""327 Miami Court, Hillsboro, Kentucky, 2496"",
    ""about"": ""Aliquip aliquip eu sint irure ut adipisicing duis in laborum ipsum sint occaecat. Cillum irure consectetur pariatur dolore aliquip do. Qui incididunt commodo irure proident aliqua amet in irure eu culpa. Consectetur eu fugiat ea voluptate voluptate dolore eiusmod excepteur esse in duis. Anim enim pariatur excepteur incididunt occaecat do proident sit nostrud quis ullamco culpa cillum nisi. Culpa fugiat proident quis exercitation amet deserunt.\r\n"",
    ""registered"": ""2015-03-04T12:16:27 +08:00"",
    ""latitude"": 0.413879,
    ""longitude"": 153.043733,
    ""tags"": [
      ""id"",
      ""duis"",
      ""duis"",
      ""pariatur"",
      ""esse"",
      ""commodo"",
      ""nisi""
    ],
    ""friends"": [
      {
        ""id"": 0,
        ""name"": ""Leona Suarez""
      },
      {
        ""id"": 1,
        ""name"": ""Maria Gibson""
      },
      {
        ""id"": 2,
        ""name"": ""Rivas Gonzales""
      }
    ],
    ""greeting"": ""Hello, Mindy Velasquez! You have 4 unread messages."",
    ""favoriteFruit"": ""strawberry""
  }
]";

Microsoft.Owin.Security.DataHandler.Encoder.TextEncodings.Base64Url.Encode(System.Text.Encoding.Default.GetBytes(s)).Dump();
*/