import 'package:url_launcher/url_launcher.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  final prefs = await SharedPreferences.getInstance();
  final isLoggedIn = prefs.getBool('isLoggedIn') ?? false;
  final username = prefs.getString('username') ?? "";

  runApp(MyApp(isLoggedIn: isLoggedIn, username: username));
}

class MyApp extends StatelessWidget {
  final bool isLoggedIn;
  final String username;

  const MyApp({Key? key, required this.isLoggedIn, required this.username})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Login App',
      theme: ThemeData(
        scaffoldBackgroundColor: const Color(0xFF31363F),
        colorScheme: ColorScheme.fromSwatch(
          primarySwatch: Colors.blueGrey,
          backgroundColor: const Color(0xFF31363F),
          accentColor: const Color(0xFF80C6CA),
        ),
      ),
      home: isLoggedIn ? UserPage(username: username) : LoginPage(),
    );
  }
}

class LoginPage extends StatefulWidget {
  const LoginPage({Key? key});

  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  String encrypt(String text) {
    return base64Encode(utf8.encode(text));
  }

  Future<void> loginUser() async {
    final username = _usernameController.text;
    final password = encrypt(_passwordController.text);

    final loginEndPoint = "http://10.0.2.2:5076/api/Users/login";

    try {
      final response = await http.post(
        Uri.parse(loginEndPoint),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: jsonEncode(<String, String>{
          'email': username,
          'password': password,
        }),
      );

      if (response.statusCode == 200) {
        final prefs = await SharedPreferences.getInstance();
        prefs.setBool('isLoggedIn', true);
        prefs.setString('username', username); // Guardar el nombre de usuario
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => UserPage(username: username)),
        );
      } else {
        showDialog(
          context: context,
          builder: (context) {
            return AlertDialog(
              title: const Text('Login Failed',
                  style: TextStyle(color: Colors.white)),
              content: Text('Wrong credentials',
                  style: TextStyle(color: Colors.white)),
              actions: [
                TextButton(
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                  child: const Text('Close',
                      style: TextStyle(color: Colors.white)),
                ),
              ],
            );
          },
        );
      }
    } catch (error) {
      // Manejar cualquier error de red u otro error que pueda ocurrir
      print('Error de red al intentar iniciar sesión: $error');
      // Aquí puedes mostrar un mensaje de error al usuario
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.transparent,
      body: Stack(
        children: [
          Container(
            decoration: BoxDecoration(
              image: DecorationImage(
                image: NetworkImage(
                    "https://images.unsplash.com/photo-1580274455191-1c62238fa333?q=80&w=1364&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"),
                fit: BoxFit.cover,
              ),
            ),
          ),
          Center(
            child: Container(
              width: double.infinity,
              padding: const EdgeInsets.all(32.0),
              margin:
                  const EdgeInsets.symmetric(horizontal: 30.0, vertical: 50.0),
              decoration: BoxDecoration(
                color: Colors.white.withOpacity(0.50),
                borderRadius: BorderRadius.circular(15.0),
              ),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    'GETD',
                    style: TextStyle(
                      fontSize: 64.0,
                      fontWeight: FontWeight.bold,
                      color: Colors.black,
                      letterSpacing: 10.0,
                      shadows: [
                        Shadow(
                          offset: Offset(3.0, 3.0),
                          blurRadius: 10.0,
                          color: Colors.black.withOpacity(0.25),
                        ),
                      ],
                    ),
                  ),
                  const SizedBox(height: 50.0),
                  TextField(
                    controller: _usernameController,
                    decoration: InputDecoration(
                      hintText: 'Username',
                      hintStyle: const TextStyle(
                          fontSize: 16.0, color: Colors.black54),
                      filled: true,
                      fillColor: Colors.white,
                      contentPadding: const EdgeInsets.symmetric(
                          horizontal: 16.0, vertical: 12.0),
                      enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(10.0),
                        borderSide:
                            BorderSide(color: Colors.grey.shade200, width: 1.0),
                      ),
                      focusedBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(10.0),
                        borderSide: BorderSide(
                            color: const Color(0xFF80C6CA), width: 2.0),
                      ),
                    ),
                  ),
                  const SizedBox(height: 20.0),
                  TextField(
                    controller: _passwordController,
                    decoration: InputDecoration(
                      hintText: 'Password',
                      hintStyle: const TextStyle(
                          fontSize: 16.0, color: Colors.black54),
                      filled: true,
                      fillColor: Colors.white,
                      contentPadding: const EdgeInsets.symmetric(
                          horizontal: 16.0, vertical: 12.0),
                      enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(10.0),
                        borderSide:
                            BorderSide(color: Colors.grey.shade200, width: 1.0),
                      ),
                      focusedBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(10.0),
                        borderSide: BorderSide(
                            color: const Color(0xFF80C6CA), width: 2.0),
                      ),
                    ),
                    obscureText: true,
                  ),
                  const SizedBox(height: 20.0),
                  ElevatedButton(
                    onPressed: loginUser,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xFF80C6CA),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0),
                      ),
                      foregroundColor: Colors.black,
                      padding: const EdgeInsets.symmetric(
                          vertical: 12.0, horizontal: 32.0),
                    ),
                    child: const Text('Login'),
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}

class UserPage extends StatelessWidget {
  final String username;

  const UserPage({Key? key, required this.username}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xFF31363F),
      appBar: AppBar(
        backgroundColor: const Color(0xFF272A2E),
        elevation: 0.0,
        title: Text(
          'User Details',
          style: TextStyle(
            fontSize: 24.0,
            fontWeight: FontWeight.bold,
            color: Colors.white,
            shadows: [
              Shadow(
                offset: Offset(2.0, 2.0),
                blurRadius: 5.0,
                color: Colors.black.withOpacity(0.5),
              ),
            ],
          ),
        ),
        centerTitle: true,
        actions: [
          IconButton(
            icon: Icon(Icons.logout),
            onPressed: () async {
              final prefs = await SharedPreferences.getInstance();
              prefs.setBool('isLoggedIn', false);
              Navigator.pushReplacement(
                context,
                MaterialPageRoute(builder: (context) => LoginPage()),
              );
            },
          ),
        ],
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            FutureBuilder<Map<String, dynamic>>(
              future: fetchUserData(username),
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return CircularProgressIndicator();
                } else if (snapshot.hasError) {
                  return Text('Error: ${snapshot.error}');
                } else {
                  final userData = snapshot.data!;
                  return Column(
                    children: [
                      Container(
                        decoration: BoxDecoration(
                          shape: BoxShape.circle,
                          color: Colors.white,
                          boxShadow: [
                            BoxShadow(
                              offset: Offset(4.0, 4.0),
                              blurRadius: 10.0,
                              spreadRadius: 2.0,
                              color: Colors.black.withOpacity(0.3),
                            ),
                          ],
                        ),
                        child: ClipOval(
                          child: Image.memory(
                            base64Decode(userData['profilePicture']),
                            width: 100.0,
                            height: 100.0,
                            fit: BoxFit.cover,
                          ),
                        ),
                      ),
                      const SizedBox(height: 20.0),
                      Text(
                        'Username: ${userData['username']}',
                        style: TextStyle(
                          fontSize: 18.0,
                          fontWeight: FontWeight.w500,
                          color: Colors.white,
                        ),
                      ),
                      const SizedBox(height: 10.0),
                      Text(
                        'Email: ${userData['email']}',
                        style: TextStyle(
                          fontSize: 16.0,
                          color: const Color(0xFFEEEEEE),
                        ),
                      ),
                      const SizedBox(height: 20.0),
                      ElevatedButton(
                        onPressed: () {
                          _showCarBrandBottomSheet(context);
                        },
                        style: ElevatedButton.styleFrom(
                          foregroundColor: Colors.white,
                          backgroundColor: const Color(0xFF80C6CA),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(20.0),
                          ),
                          padding: const EdgeInsets.symmetric(
                              horizontal: 24.0, vertical: 12.0),
                        ),
                        child: Text(
                          'Select Car Brand',
                          style: const TextStyle(fontSize: 16.0),
                        ),
                      ),
                    ],
                  );
                }
              },
            ),
          ],
        ),
      ),
    );
  }

  Future<Map<String, dynamic>> fetchUserData(String username) async {
    final response = await http.get(
      Uri.parse('http://10.0.2.2:5076/api/Users/Get-user?username=$username'),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception('Failed to load user data');
    }
  }

  void _showCarBrandBottomSheet(BuildContext context) async {
    final response = await http
        .get(Uri.parse('http://10.0.2.2:5076/api/Data/get-car-brands'));

    if (response.statusCode == 200) {
      List<dynamic> brands = json.decode(response.body);

      showModalBottomSheet(
        context: context,
        backgroundColor: const Color(0xFF272A2E),
        isScrollControlled: true,
        builder: (BuildContext context) {
          return _CarBrandBottomSheet(brands: brands);
        },
      );
    } else {
      showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: Text("Error"),
            content: Text("Failed to load car brands."),
            actions: [
              TextButton(
                onPressed: () => Navigator.pop(context),
                child: Text("OK"),
              ),
            ],
          );
        },
      );
    }
  }
}

class _CarBrandBottomSheet extends StatefulWidget {
  final List<dynamic> brands;

  const _CarBrandBottomSheet({Key? key, required this.brands})
      : super(key: key);

  @override
  _CarBrandBottomSheetState createState() => _CarBrandBottomSheetState();
}

class _CarBrandBottomSheetState extends State<_CarBrandBottomSheet> {
  late List<dynamic> filteredBrands;
  final TextEditingController _searchController = TextEditingController();

  @override
  void initState() {
    super.initState();
    filteredBrands = widget.brands;
  }

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      padding: EdgeInsets.only(
        bottom: MediaQuery.of(context).viewInsets.bottom,
      ),
      child: Container(
        height: MediaQuery.of(context).size.height * 0.4,
        child: Column(
          children: [
            Padding(
              padding: const EdgeInsets.all(20),
              child: TextField(
                controller: _searchController,
                decoration: InputDecoration(
                  hintText: 'Search brands...',
                  hintStyle: TextStyle(
                    color: const Color.fromARGB(255, 47, 47, 47),
                  ),
                  prefixIcon: Icon(
                    Icons.search,
                    color: Color(0xFF80C6CA),
                  ),
                  filled: true,
                  fillColor: Color.fromARGB(132, 255, 255, 255),
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(8.0),
                    borderSide: BorderSide.none,
                  ),
                  contentPadding: EdgeInsets.symmetric(vertical: 10),
                ),
                onChanged: (value) {
                  setState(() {
                    filteredBrands = widget.brands
                        .where((brand) =>
                            brand.toLowerCase().contains(value.toLowerCase()))
                        .toList();
                  });
                },
              ),
            ),
            Expanded(
              child: ListView.builder(
                itemCount: filteredBrands.length,
                itemBuilder: (BuildContext context, int index) {
                  return ListTile(
                    title: Text(
                      filteredBrands[index],
                      style: TextStyle(
                        color: Colors.white,
                      ),
                    ),
                    onTap: () {
                      Navigator.pop(context);
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) =>
                              CarListPage(selectedBrand: filteredBrands[index]),
                        ),
                      );
                    },
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class CarListPage extends StatelessWidget {
  final String selectedBrand;

  const CarListPage({Key? key, required this.selectedBrand}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xFF31363F),
      appBar: AppBar(
        backgroundColor: const Color(0xFF272A2E),
        elevation: 0.0,
        title: Text(
          'Cars by $selectedBrand',
          style: TextStyle(
            fontSize: 24.0,
            fontWeight: FontWeight.bold,
            color: Colors.white,
            shadows: [
              Shadow(
                offset: Offset(2.0, 2.0),
                blurRadius: 5.0,
                color: Colors.black.withOpacity(0.5),
              ),
            ],
          ),
        ),
        centerTitle: true,
      ),
      body: FutureBuilder<List<String>>(
        future: fetchCarsByBrand(selectedBrand),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(
              child: CircularProgressIndicator(
                color: Colors.teal,
              ),
            );
          } else if (snapshot.hasError) {
            return Center(
              child: Text(
                'Error: ${snapshot.error}',
                style: TextStyle(color: Colors.redAccent),
              ),
            );
          } else {
            return ListView.builder(
              itemCount: snapshot.data!.length,
              itemBuilder: (context, index) {
                final carModel = snapshot.data![index];
                return Column(
                  children: [
                    ListTile(
                      title: Text(
                        carModel,
                        style: TextStyle(
                          fontWeight: FontWeight.w500,
                          color: Colors.white,
                        ),
                      ),
                      trailing: TextButton(
                        onPressed: () {
                          launchUrl(Uri.parse(
                              'https://www.google.com/search?q=$carModel'));
                        },
                        child: Text(
                          "+ info",
                          style: TextStyle(
                            color: const Color(0xFF80C6CA),
                          ),
                        ),
                      ),
                    ),
                    Divider(
                      color: Colors.grey.shade800,
                      thickness: 1.0,
                    ),
                  ],
                );
              },
            );
          }
        },
      ),
    );
  }

  Future<List<String>> fetchCarsByBrand(String brand) async {
    final response = await http.get(Uri.parse(
        'http://10.0.2.2:5076/api/Data/get-cars-by-brand?make=$brand'));

    if (response.statusCode == 200) {
      return List<String>.from(jsonDecode(response.body));
    } else {
      throw Exception('Failed to load cars for brand $brand');
    }
  }
}
