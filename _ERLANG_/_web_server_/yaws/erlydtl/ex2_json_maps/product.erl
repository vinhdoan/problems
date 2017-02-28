-module(product).
-compile(export_all).

create_product() ->
    #{id => 483,
      product_code => "C7HR-BCH",
      brand => "Schecter",
      description => "Schecter C7HR-BCH",
      category => #{name => "electric guitar",
                   category => #{name => "7 string model"}},
      frets => 24,
      body => "mahogany",
      pickup => "2x EMG 707TW"}.
