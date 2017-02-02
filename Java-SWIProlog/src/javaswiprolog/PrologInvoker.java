/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package javaswiprolog;
import org.jpl7.*;
import java.util.Map;
import java.io.*;
import java.util.Date;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
/**
 *
 * @author tdvtnt
 */
public class PrologInvoker {
    private int noOfClasses = 6;
    private int noOfOptions = 5;
    private int[][] tableOfOptions = {{1,0,1,1,0},{0,0,0,1,0},{0,1,0,0,1},{0,1,0,1,0},{1,0,1,0,0},{1,1,0,0,0}};
    private int[] noOfCars = {1,1,2,2,2,2};
    private int[][] capacityOfOption = {{1,2},{2,3},{1,3},{2,5},{1,5}};
    private int totalCars = 10;
    private int[] noOfCarsHasTheOption = {5,6,3,4,2};
    
    public PrologInvoker() {
    }
    public PrologInvoker(int noOfClasses, int noOfOptions, int[][] tableOfOptions, int[] noOfCars, int[][] capacityOfOption) {
        this.noOfClasses = noOfClasses;
        this.noOfOptions = noOfOptions;
        this.tableOfOptions = tableOfOptions;
        this.noOfCars = noOfCars;
        this.capacityOfOption = capacityOfOption;
        
        //Calculate total cars
        int sum = 0;
        for (int i = 0; i < noOfCars.length; i++) {
            sum += noOfCars[i];
        }
        this.totalCars = sum;
        
        //Calculate total cars for each option
        int[] tmp = new int[noOfOptions];
        for (int i = 0; i < noOfClasses; i++) {
            for (int j = 0; j < noOfOptions; j++) {
                if (tableOfOptions[i][j] == 1) {
                    tmp[j] += noOfCars[i];
                }
            }
        }
        this.noOfCarsHasTheOption = tmp;
    }
    
    public String prolog_src_gen() {
        DateFormat df = new SimpleDateFormat("yyyyMMdd_HHmmss");
        Date dateobj = new Date();
        String srcFileName = "carseq_" + df.format(dateobj) + ".pl";
        FileOutputStream out;
        PrintStream prt;
        try {
            out = new FileOutputStream(srcFileName);
            prt = new PrintStream(out);
            // SOURCE CONTENT
            prt.println(":- use_module(library(clpfd)).");
            prt.println("/* Name  : " + srcFileName);
            prt.println("   Title : Car Sequencing Problem");
            prt.println("   Car sequencing problem with " + totalCars + " cars */");
            prt.println();
            prt.println("sol(X) :-");
            prt.println("\tcars(X),");
            
            ////Demand Constraints: atmost
            for (int i = 0; i < noOfClasses - 1; i++) {
                prt.println("\tatmost(" + noOfCars[i] + ",X," + (i + 1) + "),");
            }
            prt.println("\tatmost(" + noOfCars[noOfClasses - 1] + ",X," + noOfClasses + ").");
            prt.println();
            
            prt.println("cars(X):-");

            ////Problem Variables: X=[X1,X2,X3,X4,X5,X6,X7,X8,X9,X10],
            prt.print("\tX=[X1");
            for (int i = 1; i < totalCars; i++) {
                prt.print(",X" + (i + 1));
            }
            prt.println("],");
            prt.println();
            
            ////Problem Variables: Y=[...]
            prt.println("\tY=[");
            for (int i = 1; i < totalCars; i++) {
                prt.print("\t");
                for (int j = 0; j < noOfOptions; j++) {
                    prt.print("O" + i + "_" + (j + 1) + ",");
                }
                prt.println();
            }
            prt.print("\tO" + totalCars + "_1");
            for (int i = 1; i < noOfOptions; i++) {
                prt.print(",O" + totalCars + "_" + (i + 1));
            }
            prt.println("],");
            prt.println();

            ////Problem Variables: Lx=[...]
            for (int i = 0; i < noOfOptions; i++) {
                prt.print("\tL" + (i + 1) + "=[");
                for (int j = 0; j < noOfClasses - 1; j++) {
                    prt.print(tableOfOptions[j][i] + ",");
                }
                prt.println(tableOfOptions[noOfClasses-1][i] + "],");
            }
            prt.println();

            ////Domain Constraints: X,Y ins <domain>
            prt.println("\tY ins 0..1,");
            prt.println("\tX ins 1.." + noOfClasses + ",");
            prt.println();
            
            ////Link Constraints: element(X,L,O)
            for (int i = 0; i < totalCars; i++) {
                for (int j = 0; j < noOfOptions; j++) {
                    prt.println("\telement(X" + (i + 1) + ",L" + (j + 1) + ",O" + (i + 1) + "_" + (j + 1) + "),");
                }
            }
            prt.println();
            
            ////Capacity Constraints
            for (int i = 0; i < noOfOptions; i++) {
                for (int j = 0; j < totalCars - capacityOfOption[i][1] + 1; j++) {
                    prt.print("\t" + capacityOfOption[i][0] + " #>= O" + (j + 1) + "_" + (i + 1));
                    for (int k = 0; k < capacityOfOption[i][1] - 1; k++) {
                        prt.print(" + O" + (j + k + 2) + "_" + (i + 1));
                    }
                    prt.println(",");
                }
            }
            prt.println();
            
            ////Redundant Constraints
            for (int j = 0; j < noOfOptions; j++) {
                for (int k = 1; k <= totalCars / capacityOfOption[j][1]; k++) {
                    if (totalCars - k * capacityOfOption[j][1] >= 1) {
                        prt.print("\tO1_" + (j + 1));
                        for (int i = 2; i <= totalCars - k * capacityOfOption[j][1]; i++) {
                            prt.print(" + O" + i + "_" + (j + 1));
                        }
                        prt.println(" #>= " + (noOfCarsHasTheOption[j] - capacityOfOption[j][0] * k) + ",");
                    }
                }
            }
            prt.println();
            
            ////Labeling and subfunctions
            prt.println("\tlabeling([],X).");
            prt.println();
            prt.println("atmost(Num, List, Val) :-");
            prt.println("\tcount_val(List, Val, Res),");
            prt.println("\tNum >= Res.");
            prt.println();
            prt.println("count_val([], _, 0).");
            prt.println("count_val([Val|T], Val, Res) :-");
            prt.println("\tcount_val(T, Val, RRes),");
            prt.println("\tRes is RRes + 1, !.");
            prt.println("count_val([_|T], Val, Res) :-");
            prt.println("\tcount_val(T, Val, Res).");
            
            prt.close();
            
            return srcFileName;
        }
        catch (Exception e) {
            System.out.println("Write error");
            return "";
        }
    }
    
    public void solve(String filePath) {
        String t1 = "consult('" + filePath + "')";
	Query q1 = new Query(t1);
        if (q1.hasSolution()) {
            String t2 = "sol(X)";
            Query q2 = new Query(t2);
            if (q2.hasSolution()) {
                System.out.println("ALL SOLUTIONS:");
                Map<String, Term> map[] = q2.allSolutions();
                for(int i = 0; i < map.length; i++) {
                    System.out.print("Solution " + (i + 1) + ": ");
                    for (int j = 0; j < map[i].get("X").listLength(); j++) {
                        System.out.print(map[i].get("X").toTermArray()[j] + " ");
                    }
                    System.out.println();
                }
            }
        }
    }
}
