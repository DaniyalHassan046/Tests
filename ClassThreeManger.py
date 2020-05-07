class Manage:
    value_list = list()
    categories = set()
    colors = set()
    brand = set()
    size = set()
    sorted_list = list()
    # Function for Sorting
    def Sort(self, sorted_list1, asc, index_number):
        return sorted(sorted_list1, key=lambda x: x[int(index_number)], reverse=asc)

    def proceeding_menu_checker(self, inp, _lists):

        if int(inp) == 1:
            self.sorted_list = self.Sort(_lists, asc=True, index_number=1)
            return self.sorted_list
        elif int(inp) == 2:
            self.sorted_list = self.Sort(_lists, asc=False, index_number=1)
            return self.sorted_list
        elif int(inp) == 3:
            self.sorted_list = self.Sort(_lists, asc=True, index_number=3)
            return self.sorted_list
        elif int(inp) == 4:
            self.sorted_list = self.Sort(_lists, asc=True, index_number=4)
            return self.sorted_list

    def filtering_method(self, filter_value, filter_value_detail, categories):
        if filter_value == "color":
            selected = list(filter_value_detail)
            inp = input("Select Color Number: ")
            print("Selected Color is: ", inp, " ", selected[int(inp) - 1])
            return selected[int(inp) - 1]
        elif filter_value == "size":
            selected = list(filter_value_detail)
            inp = input("Select Size Number: ")
            print("Selected Color is: ", inp, " ", selected[int(inp) - 1])
            return selected[int(inp) - 1]
        elif filter_value == "brand":
            print("There is no Brand in file")
        elif filter_value == "price":
            selected = list(filter_value_detail)
            return selected[int(inp) - 1]

    print("Minimum Price: ", min, " ", "Maximum Price: ", max)

    def category(self, _category_value, size_color_checker,file):
        for row in file:
            if _category_value == row["category"]:
                if row["color"] != "":
                    self.colors.add(row["color"])
                if row["size"] != "":
                    self.size.add(row["size"])
                if row["brand"] != "":
                    self.brand.add(row["color"])

        if size_color_checker == "color":
            return self.colors
        elif size_color_checker == "size":
            return self.size
        elif size_color_checker == "price":
            return "price"
        elif size_color_checker == "brand":
            return self.brand

    def filtering_color_size(self,_cat_value,d,_color_or_size="", _val="color", _min_price="0", _max_price="1",
                             size_color_checker=False, ):

        for row in d:

            if _cat_value == row["category"]:
                if size_color_checker:
                    if _color_or_size == row[_val] and _cat_value == row["category"]:
                        self.value_list.append(
                            [row["product_id"], row["price"], row["title"], row["rating"], row["arrival_date"],
                             row["brand"], row["category"], row["color"], row["size"], row["material_info"]])

                    elif int(_min_price) <= int(row["price"]) <= int(_max_price):
                        self.value_list.append(
                            [row["product_id"], row["price"], row["title"], row["rating"], row["arrival_date"],
                             row["brand"], row["category"], row["color"], row["size"], row["material_info"]])

        return self.value_list

    def category_values(self, data):
        print("Following are the Categories in our Website. Please Select One of them using number")
        for row in data:
            self.categories.add(row["category"])
        return self.categories
