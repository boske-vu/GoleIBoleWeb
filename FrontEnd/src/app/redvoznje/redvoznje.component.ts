import { Component, OnInit } from '@angular/core';
import { TimetableType } from '../classes/timetableType';
import { Timetable } from '../classes/timetable';
import { Line } from '../classes/line';
import { DayType } from '../classes/dayType';
import { RedVoznjeInfo } from '../classes/redVoznjeInfo';
import { RedVoznjeHttpService } from '../services/redvoznje.service';

@Component({
  selector: 'app-redvoznje',
  templateUrl: './redvoznje.component.html',
  styleUrls: ['./redvoznje.component.css']
})
export class RedvoznjeComponent implements OnInit {

  redVoznjeInfo:RedVoznjeInfo = new RedVoznjeInfo();
  selectedTimetableType: TimetableType = new TimetableType();
  selectedDayType: DayType = new DayType();
  selectedLine: Line = new Line();
  filteredLines: Line[] = [];
  timetable: Timetable = new Timetable();

  constructor(private http: RedVoznjeHttpService) { }

  ngOnInit() {
    this.http.getAll().subscribe((redVoznjeInfo) => {
      this.redVoznjeInfo = redVoznjeInfo;
      err => console.log(err);
    });
  }

  changeselectedLine(){
    this.filteredLines.splice(0);
    this.redVoznjeInfo.Lines.forEach(element => {
      if(element.SerialNumber == this.selectedLine.SerialNumber){
        this.filteredLines.push(element);
      }
    });
  }

  ispisPolaska(){
    this.http.getSelected(this.selectedTimetableType.Id, this.selectedDayType.Id,this.selectedLine.Id).subscribe((data)=>{
      this.timetable.Times = data;
      console.log(this.timetable);
      err => console.log(err);
    });
  }

}
