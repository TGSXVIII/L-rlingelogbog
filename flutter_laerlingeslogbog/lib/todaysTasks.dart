import 'package:flutter/material.dart';
import 'package:flutter_laerlingeslogbog/global.dart';
import 'package:flutter_laerlingeslogbog/templates/footer.dart';
import 'package:flutter_laerlingeslogbog/templates/headerWithBack.dart';
import 'package:horizontal_list_calendar/horizontal_list_calendar.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Læringslogbog',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(
          seedColor: const Color.fromARGB(255, 255, 255, 255),
        ),
        useMaterial3: true,
      ),
      home: const TodaysTasks(title: 'TodaysTasks'),
    );
  }
}

class TodaysTasks extends StatefulWidget {
  const TodaysTasks({super.key, required this.title});
  final String title;

  @override
  State<TodaysTasks> createState() => _TodaysTasksState();
}

class _TodaysTasksState extends State<TodaysTasks> {
  late final ScrollController _calendarScrollController;
  DateTime? _selectedDate;
  final List<String> titles = ['alle', 'to do', 'igang', 'færdig'];
  final List<String> taskStatus = ['to do', 'igang', 'færdig'];

  @override
  void initState() {
    super.initState();
    _calendarScrollController = ScrollController();
    _selectedDate = DateTime.now();
  }

  @override
  void dispose() {
    _calendarScrollController.dispose();
    super.dispose();
  }

  Widget _buildCalendarWidget() {
    try {
      return HorizontalListCalendar(
        scrollController: _calendarScrollController,
        onTap: (selectedDate) {
          debugPrint('Selected Date: $selectedDate');
          setState(() {
            _selectedDate = selectedDate;
          });
        },
        selectedColor: Global.primaryColor,
        iconSize: 24,
        moveToPreviousMonthIcon: const Icon(Icons.arrow_back),
        moveToNextMonthIcon: const Icon(Icons.arrow_forward),
        curve: Curves.easeInOut,
        duration: const Duration(milliseconds: 500),
      );
    } catch (e, stackTrace) {
      debugPrint('Calendar error: $e');
      debugPrint('Stack trace: $stackTrace');
      return _buildFallbackCalendar();
    }
  }

  Widget _buildFallbackCalendar() {
    final currentDate = _selectedDate ?? DateTime.now();
    final monthNames = [
      'Januar',
      'Februar',
      'Marts',
      'April',
      'Maj',
      'Juni',
      'Juli',
      'August',
      'September',
      'Oktober',
      'November',
      'December'
    ];
    final dayNames = ['Man', 'Tir', 'Ons', 'Tor', 'Fre', 'Lør', 'Søn'];

    return Container(
      height: 140,
      padding: const EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
        border: Border.all(color: Global.primaryColor.withOpacity(0.3)),
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.1),
            blurRadius: 10,
            offset: const Offset(0, 2),
          ),
        ],
      ),
      child: Column(
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              IconButton(
                onPressed: () {
                  setState(() {
                    _selectedDate = DateTime(currentDate.year,
                        currentDate.month - 1, currentDate.day);
                  });
                },
                icon: Icon(Icons.chevron_left, color: Global.primaryColor),
                style: IconButton.styleFrom(
                  backgroundColor: Global.primaryColor.withOpacity(0.1),
                  shape: const CircleBorder(),
                ),
              ),
              Column(
                children: [
                  Text(
                    '${monthNames[currentDate.month - 1]}',
                    style: const TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                      color: Colors.black87,
                    ),
                  ),
                  Text(
                    '${currentDate.year}',
                    style: TextStyle(
                      fontSize: 14,
                      color: Colors.grey[600],
                    ),
                  ),
                ],
              ),
              IconButton(
                onPressed: () {
                  setState(() {
                    _selectedDate = DateTime(currentDate.year,
                        currentDate.month + 1, currentDate.day);
                  });
                },
                icon: Icon(Icons.chevron_right, color: Global.primaryColor),
                style: IconButton.styleFrom(
                  backgroundColor: Global.primaryColor.withOpacity(0.1),
                  shape: const CircleBorder(),
                ),
              ),
            ],
          ),
          const SizedBox(height: 16),

          Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: dayNames.map((day) {
              return SizedBox(
                width: 32,
                child: Text(
                  day,
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    fontSize: 12,
                    fontWeight: FontWeight.w500,
                    color: Colors.grey[700],
                  ),
                ),
              );
            }).toList(),
          ),
          const SizedBox(height: 8),

          Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: List.generate(7, (index) {
              final dayOffset = index - currentDate.weekday + 1;
              final dayDate = currentDate.add(Duration(days: dayOffset));
              final isToday = dayDate.day == DateTime.now().day &&
                  dayDate.month == DateTime.now().month &&
                  dayDate.year == DateTime.now().year;
              final isSelected = _selectedDate != null &&
                  dayDate.day == _selectedDate!.day &&
                  dayDate.month == _selectedDate!.month &&
                  dayDate.year == _selectedDate!.year;

              return GestureDetector(
                onTap: () {
                  setState(() {
                    _selectedDate = dayDate;
                  });
                },
                child: Container(
                  width: 32,
                  height: 32,
                  alignment: Alignment.center,
                  decoration: BoxDecoration(
                    color: isSelected
                        ? Global.primaryColor
                        : isToday
                            ? Global.primaryColor.withOpacity(0.1)
                            : Colors.transparent,
                    shape: BoxShape.circle,
                    border: isToday && !isSelected
                        ? Border.all(
                            color: Global.primaryColor.withOpacity(0.3),
                            width: 1,
                          )
                        : null,
                  ),
                  child: Text(
                    '${dayDate.day}',
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: isSelected || isToday
                          ? FontWeight.bold
                          : FontWeight.normal,
                      color: isSelected
                          ? Colors.white
                          : isToday
                              ? Global.primaryColor
                              : Colors.black87,
                    ),
                  ),
                ),
              );
            }),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final ongoingItems = List.generate(titles.length, (i) => i);
    final measureItems = List.generate(4, (i) => i);

    return Scaffold(
      appBar: const HeaderWithBack(),
      body: SafeArea(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(16),
          child: Column(
            children: <Widget>[
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  _buildCalendarWidget(),
                ],
              ),

              const SizedBox(height: 24),

              SizedBox(
                height: 60,
                child: SingleChildScrollView(
                  scrollDirection: Axis.horizontal,
                  child: Row(
                    children: List.generate(ongoingItems.length, (index) {
                      final title = titles[index];
                      
                      return Padding(
                        padding: EdgeInsets.only(
                          right: index == ongoingItems.length - 1 ? 0 : 8,
                        ),
                        child: Container(
                          height: 50,
                          constraints: BoxConstraints(
                            minWidth: 80,
                            maxWidth: 120,
                          ),
                          padding: const EdgeInsets.symmetric(
                            horizontal: 16,
                            vertical: 8,
                          ),
                          decoration: BoxDecoration(
                            color: Global.primaryColor,
                            borderRadius: BorderRadius.circular(12),
                          ),
                          child: Center(
                            child: Text(
                              title,
                              style: const TextStyle(
                                color: Colors.white,
                                fontSize: 14,
                                fontWeight: FontWeight.w600,
                              ),
                              maxLines: 1,
                              overflow: TextOverflow.ellipsis,
                            ),
                          ),
                        ),
                      );
                    }),
                  ),
                ),
              ),

              const SizedBox(height: 24),

              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const SizedBox(height: 16),
                  ...List.generate(measureItems.length, (index) {
                    // Get a different status for each card
                    final statusIndex = index % taskStatus.length;
                    final status = taskStatus[statusIndex];
                    
                    return Padding(
                      padding: const EdgeInsets.only(bottom: 12),
                      child: _MeasureCard(
                        title: "Målepinde ${index + 1}",
                        subtitle: "opgave titel",
                        status: status,
                        color: Colors.white,
                      ),
                    );
                  }),
                ],
              ),
            ],
          ),
        ),
      ),
      bottomNavigationBar: const Footer(),
    );
  }
}

class _MeasureCard extends StatelessWidget {
  final String title;
  final String subtitle;
  final String status;
  final Color color;

  const _MeasureCard({
    required this.title,
    required this.subtitle,
    required this.status,
    this.color = Colors.white,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 80,
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
      decoration: BoxDecoration(
        color: color,
        borderRadius: BorderRadius.circular(20),
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.1),
            blurRadius: 8,
            offset: const Offset(0, 2),
          ),
        ],
      ),
      child: Stack(
        children: [
          Row(
            children: [
              Expanded(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      title,
                      maxLines: 1,
                      overflow: TextOverflow.ellipsis,
                      style: const TextStyle(
                        color: Colors.black,
                        fontSize: 13,
                      ),
                    ),
                    const SizedBox(height: 4),
                    Text(
                      subtitle,
                      maxLines: 1,
                      overflow: TextOverflow.ellipsis,
                      style: TextStyle(
                        color: Colors.black.withOpacity(0.9),
                        fontSize: 16,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
          
          // Status badge in bottom right corner
          Positioned(
            bottom: 8,
            right: 8,
            child: Container(
              padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
              decoration: BoxDecoration(
                color: _getStatusColor(status).withOpacity(0.1),
                borderRadius: BorderRadius.circular(8),
                border: Border.all(
                  color: _getStatusColor(status).withOpacity(0.3),
                  width: 1,
                ),
              ),
              child: Text(
                status,
                style: TextStyle(
                  color: _getStatusColor(status),
                  fontSize: 10,
                  fontWeight: FontWeight.w600,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  // Helper method to get status color
  Color _getStatusColor(String status) {
    switch (status.toLowerCase()) {
      case 'to do':
        return Colors.orange;
      case 'igang':
        return Colors.blue;
      case 'færdig':
        return Colors.green;
      default:
        return Colors.grey;
    }
  }
}