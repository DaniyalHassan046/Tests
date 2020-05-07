from ClassOneReader import Reader
from CLassTwoShow import Show
from ClassThreeManger import Manage
from ClassFourInput import Input

# Driver
# print("----------------------Products------------------------")
#   print("Welcome to Products Site. Press enter to proceed.")
#   input()
input_file = dict()
categories = set()
filtered_value = set()

temp = list()
r = Reader()
p = Reader()
i = Input()
s = Show()
m = Manage()
input_file=r.get_file()
file=list(input_file)
categories = m.category_values(file)
s.set_show(categories)
categories = i.category_input(categories)
a = str(i.filter_via())  # a=color,size etc
filtered_value = m.category(categories, a,file)  # selected filter values = details of color/size etc
if filtered_value is not None and filtered_value != "price":
    if s.set_show(filtered_value):

        temp = m.filtering_color_size(categories,file, m.filtering_method(a, filtered_value, categories),
                                      a,
                                      size_color_checker=True)
        temp = s.table(m.proceeding_menu_checker(i.proceeding_menu(), temp))
        s.id_show(i.id_selector(), temp)
    else:
        print("Nothing Found")

elif filtered_value == "price":
    minimum = input("Enter minimum Price: ")
    maximum = input("Enter maximum Price: ")
    temp = m.filtering_color_size(categories,file, _min_price=minimum, _max_price=maximum,
                                  size_color_checker=True)
    temp = s.table(m.proceeding_menu_checker(i.proceeding_menu(), temp))
    s.id_show(i.id_selector(), temp)
else:
    print("There is no product in this filtering")
# self.proceeding_menu_checker(inp)
