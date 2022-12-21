import 'package:equatable/equatable.dart';

class BaseModel with EquatableMixin {
  final String id;
  final DateTime dateCreated;
  final DateTime dateModified;

  const BaseModel(this.id, this.dateCreated, this.dateModified);

  @override
  List<Object?> get props {
    return [id, dateCreated, dateModified];
  }
}
