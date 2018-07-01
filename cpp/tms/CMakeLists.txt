include_directories(${CMAKE_CURRENT_SOURCE_DIR}/../libtms/include)
add_executable (tms cpp/main.cpp)
target_link_libraries (tms libtms)