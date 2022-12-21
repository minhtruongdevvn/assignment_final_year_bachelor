class PageResult<TResult> {
  final List<TResult> results;
  final int currentPage;
  final int pageCount;
  final int pageSize;
  final int pageRowCount;
  final int rowCount;
  final int firstRowOnPage;
  final int lastRowOnPage;
  final bool hasPreviousPage;
  final bool hasNextPage;

  PageResult(
      this.results,
      this.currentPage,
      this.pageCount,
      this.pageSize,
      this.rowCount,
      this.firstRowOnPage,
      this.lastRowOnPage,
      this.hasPreviousPage,
      this.hasNextPage,
      this.pageRowCount);

  factory PageResult.fromJson(Map<String, dynamic> json,
          TResult Function(Map<String, dynamic>) deserializer) =>
      PageResult(
        json["results"] == null
            ? []
            : List<Map<String, dynamic>>.from(json["results"])
                .map((e) => deserializer(e))
                .toList(),
        json["currentPage"] as int,
        json["pageCount"] as int,
        json["pageSize"] as int,
        json["rowCount"] as int,
        json["firstRowOnPage"] as int,
        json["lastRowOnPage"] as int,
        json["hasPreviousPage"] as bool,
        json["hasNextPage"] as bool,
        json["pageRowCount"] as int,
      );
}

enum FilterOperation { equal, contain, lessOrEqual, greaterOrEqual }

extension FilterOperationExtension on FilterOperation {
  String getOperator() {
    switch (this) {
      case FilterOperation.equal:
        return '==';
      case FilterOperation.contain:
        return '@=';
      case FilterOperation.lessOrEqual:
        return '<=';
      case FilterOperation.greaterOrEqual:
        return '>=';
    }
  }
}

class QueryBuilder {
  Map<String, String> _filters = {};
  String _sort = '';
  int _page = 1;
  int _pageSize;

  QueryBuilder([this._pageSize = 10]);

  void switchSort(String field) => _sort = field;

  void switchPage(int page) => _page = page;

  void switchPageSize(int size) => _pageSize = size;

  void addFilter(
    String field,
    String? value, {
    FilterOperation operation = FilterOperation.equal,
    String appendix = '',
  }) {
    if (value == null || value.isEmpty) {
      _filters.remove(field);
      return;
    }

    _filters.update(field, (prevValue) {
      if (prevValue.contains(operation.getOperator())) {
        return '${operation.getOperator()}$value$appendix';
      } else {
        return '$prevValue,$field${operation.getOperator()}$value$appendix';
      }
    }, ifAbsent: () => '${operation.getOperator()}$value$appendix');
  }

  void resetSort() {
    _sort = '';
  }

  void resetFilter() {
    _filters = {};
  }

  void resetBuilding() {
    _filters = {};
    _sort = '';
    _page = 1;
    _pageSize = 12;
  }

  String build() {
    final filterSubString = _filters.isEmpty
        ? ''
        : _filters.entries
            .map((e) => '${e.key}${e.value}')
            .reduce((curr, next) => '$curr,$next');

    final buffer = StringBuffer();

    buffer.write('?');
    buffer.write('PageSize=$_pageSize');
    buffer.write('&Page=$_page');
    buffer.write(_sort.isEmpty ? '' : '&Sorts=$_sort');
    buffer.write(filterSubString.isEmpty ? '' : '&Filters=$filterSubString');

    return buffer.toString();
  }
}
