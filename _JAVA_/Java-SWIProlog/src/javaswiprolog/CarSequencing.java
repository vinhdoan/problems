/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package javaswiprolog;
import org.jpl7.*;
import java.util.Scanner;
import java.io.File;
/**
 *
 * @author Tuan Tran
 */
public class CarSequencing {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        PrologInvoker pi;
        Scanner in = new Scanner(System.in);
        System.out.print("Press 'd' for default test, other keys for normally running: ");
        if (in.next().charAt(0) == 'd') {
            pi = new PrologInvoker();
            System.out.println(System.getProperty("user.dir"));
        }
        else {
            int noOfClasses, noOfOptions;
            System.out.print("Number of classes: ");
            noOfClasses = in.nextInt();
            System.out.print("Number of options: ");
            noOfOptions = in.nextInt();
            int[][] tableOfOptions = new int[noOfClasses][noOfOptions];
            int[] noOfCars = new int[noOfClasses];
            int[][] capacityOfOption = new int[noOfOptions][2];
            for(int i = 0; i < noOfClasses; i++) {
                System.out.println("DEFINE CLASS " + (i + 1) + "!!!");
                for(int j = 0; j < noOfOptions; j++) {
                    System.out.print("Option " + (j + 1) + "? (y/n): ");
                    tableOfOptions[i][j] = (in.next().charAt(0) == 'y'?1:0);
                }
                System.out.print("Total numbers: ");
                noOfCars[i] = in.nextInt();
            }
            for(int i = 0; i < noOfOptions; i++) {
                System.out.println("Capacity of option " + (i + 1) + " (m/M): ");
                capacityOfOption[i][0] = in.nextInt();
                capacityOfOption[i][1] = in.nextInt();
            }
            in.close();
            
            pi = new PrologInvoker(noOfClasses, noOfOptions, tableOfOptions, noOfCars, capacityOfOption);
        }
        String src = pi.prolog_src_gen();
        Boolean waitforfile = true;  
        while (waitforfile) {
            File f = new File(src);
            if (f.exists()) {
                waitforfile = false;
            }
        } 
        System.out.println(src);
        pi.solve(src);
    }
}
