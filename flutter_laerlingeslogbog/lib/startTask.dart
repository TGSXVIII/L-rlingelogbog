import 'package:flutter/material.dart';
import 'package:flutter_laerlingeslogbog/templates/footer.dart';
import 'package:flutter_laerlingeslogbog/templates/headerWithBack.dart';
import 'package:flutter_laerlingeslogbog/global.dart';
import 'package:flutter_laerlingeslogbog/todaysTasks.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Læringslogbog',
      theme: ThemeData(useMaterial3: true),
      home: const StartTask(title: 'startTask'),
    );
  }
}

class StartTask extends StatefulWidget {
  const StartTask({super.key, required this.title});
  final String title;

  @override
  State<StartTask> createState() => _StartTaskState();
}

class _StartTaskState extends State<StartTask> {
  final List<String> educationalStandards = <String>[
    'Bremser',
    'Motor',
    'Hjul',
    'Indmad',
    'Udmad',
  ];
  final List<String> projectName = <String>[
    'Skrift bremserklodser',
    'Presse bremsekaliber stemplet tilbage',
    'Tilpas bremse til vejgrebet',
  ];

  late String dropdownValue;

  @override
  void initState() {
    super.initState();
    dropdownValue = educationalStandards.first;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const HeaderWithBack(),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 15),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              const SizedBox(height: 24),
              Container(
                width: double.infinity,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  boxShadow: [
                    BoxShadow(
                      blurRadius: 12,
                      offset: const Offset(0, 6),
                      color: Colors.black.withOpacity(0.06),
                    ),
                  ],
                ),
                child: DropdownMenu<String>(
                  expandedInsets: EdgeInsets.zero,
                  initialSelection: dropdownValue,
                  onSelected: (String? value) {
                    if (value == null) return;
                    setState(() => dropdownValue = value);
                  },
                  dropdownMenuEntries: educationalStandards
                      .map((s) => DropdownMenuEntry<String>(value: s, label: s))
                      .toList(),
                  label: const Text("Vælg målepinde"),
                  textStyle: const TextStyle(fontWeight: FontWeight.bold),
                  inputDecorationTheme: const InputDecorationTheme(
                    border: InputBorder.none,
                    enabledBorder: InputBorder.none,
                    focusedBorder: InputBorder.none,
                    isDense: true,
                    contentPadding: EdgeInsets.zero,
                  ),
                ),
              ),
              const SizedBox(height: 24),
              Container(
                width: double.infinity,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  boxShadow: [
                    BoxShadow(
                      blurRadius: 12,
                      offset: const Offset(0, 6),
                      color: Colors.black.withOpacity(0.06),
                    ),
                  ],
                ),
                child: DropdownMenu<String>(
                  expandedInsets: EdgeInsets.zero,
                  initialSelection: dropdownValue,
                  onSelected: (String? value) {
                    if (value == null) return;
                    setState(() => dropdownValue = value);
                  },
                  dropdownMenuEntries: projectName
                      .map((s) => DropdownMenuEntry<String>(value: s, label: s))
                      .toList(),
                  label: const Text("Opgave titel"),
                  textStyle: const TextStyle(fontWeight: FontWeight.bold),
                  inputDecorationTheme: const InputDecorationTheme(
                    border: InputBorder.none,
                    enabledBorder: InputBorder.none,
                    focusedBorder: InputBorder.none,
                    isDense: true,
                    contentPadding: EdgeInsets.zero,
                  ),
                ),
              ),
              const SizedBox(height: 24),
              Container(
                width: double.infinity,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  boxShadow: [
                    BoxShadow(
                      blurRadius: 12,
                      offset: const Offset(0, 6),
                      color: Colors.black.withOpacity(0.06),
                    ),
                  ],
                ),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.start,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      "Beskrivelse",
                      style: TextStyle(fontSize: 12),
                    ),
                    Text(
                      "Test",
                      style: TextStyle(fontSize: 18),
                    ),
                  ],
                ),
              ),
              const SizedBox(height: 24),
              Container(
                width: double.infinity,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  boxShadow: [
                    BoxShadow(
                      blurRadius: 12,
                      offset: const Offset(0, 6),
                      color: Colors.black.withOpacity(0.06),
                    ),
                  ],
                ),
                child: Text("Pictures/Videos"),
              ),
              const SizedBox(height: 24),
              SizedBox(
                height: 40,
                width: MediaQuery.of(context).size.width - 30,
                child: TextButton(
                  style: TextButton.styleFrom(
                      backgroundColor: Global.primaryColor,
                      foregroundColor: Colors.white),
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) =>
                            const TodaysTasks(title: 'TodaysTasks'),
                      ),
                    );
                  },
                  child: const Text('Start opgave'),
                ),
              )
            ],
          ),
        ),
      ),
      bottomNavigationBar: const Footer(),
    );
  }
}
