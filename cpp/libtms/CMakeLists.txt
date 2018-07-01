include_directories ("${CMAKE_CURRENT_SOURCE_DIR}/include")
add_library(libtms cpp/tms.cpp cpp/infinite_list.cpp cpp/table_rw.cpp cpp/utility.cpp cpp/direction.cpp)
